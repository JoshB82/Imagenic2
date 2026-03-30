using Imagenic2.Core.Entities;
using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Renderers.Rasterising;

public partial class Rasteriser<TImage>
{
    private Queue<Triangle> triangleQueue = new Queue<Triangle>();

    /*
    private void ShadowMapRenderTriangles(RenderingEntity renderingEntity,
                                 Buffer2D<float> buffer,
                                 Matrix4x4 screenToWindow,
                                 Action<Triangle, Buffer2D<float>, int, int, float> onInterpolation)
    {
        // Reset values
        zBuffer.SetAllToValue(backgroundValue); // A number > 1

        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {
            if (physicalEntity is Mesh mesh)
            {
                if (mesh.DrawFaces)
                {
                    Matrix4x4 modelToView = renderingEntity.WorldToView * mesh.ModelToWorld;

                    foreach (Triangle triangle in mesh.Structure.Triangles)
                    {
                        triangle.TransformedP1 = new Vector4D(triangle.P1.WorldOrigin, 1);
                        triangle.TransformedP2 = new Vector4D(triangle.P2.WorldOrigin, 1);
                        triangle.TransformedP3 = new Vector4D(triangle.P3.WorldOrigin, 1);
                        
                        //TransformTriangleVertices(triangle, modelToView);

                        if (mesh.Structure.MeshDimension == MeshDimension._3D)
                        {
                            Vector3D normal = Vector3D.NormalFromPlane((Vector3D)(triangle.TransformedP1), (Vector3D)(triangle.TransformedP2), (Vector3D)(triangle.TransformedP3));
                            if ((normal * (Vector3D)(triangle.TransformedP1)).ApproxMoreThanEquals(0)) continue;
                        }
                        triangleQueue.Enqueue(triangle);
                        ClipTriangles(triangleQueue, renderingEntity.ViewClippingPlanes, ShadowMapClipTriangle);
                        if (triangleQueue.Count == 0) continue;

                        foreach (Triangle clippedTriangle in triangleQueue)
                        {
                            //TransformTriangleVertices(clippedTriangle, renderingEntity.viewToScreen);

                            clippedTriangle.invW1 = 1 / clippedTriangle.TransformedP1.w;
                            clippedTriangle.invW2 = 1 / clippedTriangle.TransformedP2.w;
                            clippedTriangle.invW3 = 1 / clippedTriangle.TransformedP3.w;

                            if (RenderingOptions.RenderCamera is PerspectiveCamera)
                            {
                                clippedTriangle.TransformedP1 /= clippedTriangle.TransformedP1.w;
                                clippedTriangle.TransformedP2 /= clippedTriangle.TransformedP2.w;
                                clippedTriangle.TransformedP3 /= clippedTriangle.TransformedP3.w;
                            }
                        }

                        //ClipTriangles(triangleQueue, Renderer<TImage>.ScreenClippingPlanes, ShadowMapClipTriangle);
                        foreach (Triangle clippedTriangle in triangleQueue)
                        {
                            //TransformTriangleVertices(clippedTriangle, screenToWindow);
                            ShadowMapInterpolateTriangle(clippedTriangle, buffer, onInterpolation, clippedTriangle.invW1, clippedTriangle.invW2, clippedTriangle.invW3);
                        }

                        triangleQueue.Clear();
                    }
                }
            }
        }
    }
    */

    private void GenerateTileTriangles(Buffer2D<Tile> tiles, RenderingEntity renderingEntity, Matrix4x4 screenToWindow, int renderWidth, int renderHeight)
    {
        foreach (PhysicalEntity physicalEntity in RenderingOptions.PhysicalEntitiesToRender)
        {
            if (physicalEntity is Mesh mesh)
            {
                if (mesh.DrawFaces)
                {
                    Matrix4x4 modelToView = renderingEntity.WorldToView * mesh.ModelToWorld;

                    foreach (Triangle triangle in mesh.Structure.Triangles)
                    {
                        ref Vertex p1 = ref triangle.p1, p2 = ref triangle.p2, p3 = ref triangle.p3;

                        p1.transformedPosition = new Vector4D(p1.WorldOrigin, 1);
                        p2.transformedPosition = new Vector4D(p2.WorldOrigin, 1);
                        p3.transformedPosition = new Vector4D(p3.WorldOrigin, 1);

                        p1.transformedTexturePosition = p1.TextureCoordinates;
                        p2.transformedTexturePosition = p2.TextureCoordinates;
                        p3.transformedTexturePosition = p3.TextureCoordinates;

                        //triangle.invW1 = 1;
                        //triangle.invW2 = 1;
                        //triangle.invW3 = 1;

                        // Transform triangle from model space to view space
                        triangle.TransformTriangleVertices(modelToView);

                        if (mesh.Structure.MeshDimension == MeshDimension._3D)
                        {
                            Vector3D normal = Vector3D.NormalFromPlane((Vector3D)(p1.transformedPosition), (Vector3D)(p2.transformedPosition), (Vector3D)(p3.transformedPosition));
                            if ((normal * (Vector3D)(p1.transformedPosition)).ApproxMoreThanEquals(0)) continue;
                        }

                        // Clip the triangle in view space
                        triangleQueue.Enqueue(triangle);
                        ClipTriangles(triangleQueue, renderingEntity.ViewClippingPlanes, ClipTriangle);
                        if (triangleQueue.Count == 0) continue;

                        foreach (Triangle clippedTriangle in triangleQueue)
                        {
                            ref Vertex clippedP1 = ref clippedTriangle.p1, clippedP2 = ref clippedTriangle.p2, clippedP3 = ref clippedTriangle.p3;

                            clippedP1.viewSpacePosition = clippedP1.transformedPosition;
                            clippedP2.viewSpacePosition = clippedP2.transformedPosition;
                            clippedP3.viewSpacePosition = clippedP3.transformedPosition;

                            clippedTriangle.TransformTriangleVertices(renderingEntity.viewToScreen);

                            clippedP1.invW = 1 / clippedP1.transformedPosition.w;
                            clippedP2.invW = 1 / clippedP2.transformedPosition.w;
                            clippedP3.invW = 1 / clippedP3.transformedPosition.w;

                            if (renderingEntity is PerspectiveCamera or Spotlight)
                            {
                                clippedP1.transformedPosition *= clippedP1.invW;
                                clippedP2.transformedPosition *= clippedP2.invW;
                                clippedP3.transformedPosition *= clippedP3.invW;

                                if (clippedTriangle.FrontStyle is TextureStyle)
                                {
                                    //clippedTriangle.TransformedTextureP1 *= clippedTriangle.invW1;
                                    //clippedTriangle.TransformedTextureP2 *= clippedTriangle.invW2;
                                    //clippedTriangle.TransformedTextureP3 *= clippedTriangle.invW3;
                                }
                            }

                            // Transform triangle from screen space to window space
                            clippedTriangle.TransformTriangleVertices(screenToWindow);

                            // Extract values
                            float x1 = clippedTriangle.p1.transformedPosition.x;
                            float y1 = clippedTriangle.p1.transformedPosition.y;
                            float z1 = clippedTriangle.p1.transformedPosition.z;
                            float x2 = clippedTriangle.p2.transformedPosition.x;
                            float y2 = clippedTriangle.p2.transformedPosition.y;
                            float z2 = clippedTriangle.p2.transformedPosition.z;
                            float x3 = clippedTriangle.p3.transformedPosition.x;
                            float y3 = clippedTriangle.p3.transformedPosition.y;
                            float z3 = clippedTriangle.p3.transformedPosition.z;

                            // Calculate triangle's bounding box
                            float minX = Min(x1, Min(x2, x3));
                            float minY = Min(y1, Min(y2, y3));
                            float maxX = Max(x1, Max(x2, x3));
                            float maxY = Max(y1, Max(y2, y3));

                            // Scale to tile size
                            int tileMinX = (int)(minX * numberOfTilesHorizontal / renderWidth);
                            int tileMinY = (int)(minY * numberOfTilesVertical / renderHeight);
                            int tileMaxX = (int)(maxX * numberOfTilesHorizontal / renderWidth);
                            int tileMaxY = (int)(maxY * numberOfTilesVertical / renderHeight);

                            // Detect tiles the triangle overlaps
                            for (int ty = tileMinY; ty <= tileMaxY; ty++)
                            {
                                for (int tx = tileMinX; tx <= tileMaxX; tx++)
                                {
                                    Tile tile = tiles[tx, ty];

                                    if (minX >= tile.startX && maxX <= tile.endX &&
                                        minY >= tile.startY && maxY <= tile.endY)
                                    {
                                        tile.triangles.Add(clippedTriangle);
                                    }
                                    else
                                    {
                                        Span<Triangle> tileClippedTriangles = ClipTriangle2D(clippedTriangle, tile.startY, tile.startX, tile.endX, tile.endY);
                                        tile.triangles.AddRange(tileClippedTriangles);
                                    }
                                }
                            }
                        }

                        triangleQueue.Clear();
                    }
                }
            }
        }
    }

    private void ShadowTileTriangles(Buffer2D<Tile> tiles, Buffer2D<float> buffer, Action<Triangle, Buffer2D<float>, int, int, float> onInterpolation)
    {
        // Reset values
        buffer.SetAllToValue(backgroundValue); // A number > 1

        // Calculate triangles
        tiles.ParallelForEach(tile =>
        {
            foreach (Triangle triangle in tile.triangles)
            {
                ShadowMapInterpolateTriangle(triangle, buffer, onInterpolation, triangle.invW1, triangle.invW2, triangle.invW3);
            }

            tile.triangles.Clear();
        });
    }

    private void RenderTileTriangles(Buffer2D<Tile> tiles, Buffer2D<float> buffer, Action<Triangle, Buffer2D<float>, int, int, float, Vector3D, Vector2D> onInterpolation)
    {
        // Reset values
        buffer.SetAllToValue(backgroundValue); // A number > 1

        // Draw triangles
        tiles.ParallelForEach(tile =>
        {
            foreach (Triangle triangle in tile.triangles)
            {
                switch (triangle.FrontStyle)
                {
                    case SolidStyle:
                        InterpolateTriangle(triangle, buffer, onInterpolation, triangle.invW1, triangle.invW2, triangle.invW3);
                        break;
                    case TextureStyle:
                        InterpolateTextureTriangle(triangle, buffer, OnTextureInterpolation, triangle.invW1, triangle.invW2, triangle.invW3);
                        break;
                }
            }

            tile.triangles.Clear();
        });
    }
}