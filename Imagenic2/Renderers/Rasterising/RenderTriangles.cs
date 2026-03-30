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
                    Span<Triangle> triangles = mesh.Structure.Triangles.AsSpan();

                    for (int i = 0; i < triangles.Length; i++)
                    {
                        triangles[i].p1.transformedPosition = new Vector4D(triangles[i].p1.WorldOrigin, 1);
                        triangles[i].p2.transformedPosition = new Vector4D(triangles[i].p2.WorldOrigin, 1);
                        triangles[i].p3.transformedPosition = new Vector4D(triangles[i].p3.WorldOrigin, 1);

                        triangles[i].p1.transformedTexturePosition = triangles[i].p1.TextureCoordinates;
                        triangles[i].p2.transformedTexturePosition = triangles[i].p2.TextureCoordinates;
                        triangles[i].p3.transformedTexturePosition = triangles[i].p3.TextureCoordinates;

                        //triangle.invW1 = 1;
                        //triangle.invW2 = 1;
                        //triangle.invW3 = 1;

                        // Transform triangle from model space to view space
                        triangles[i].TransformTriangleVertices(modelToView);

                        if (mesh.Structure.MeshDimension == MeshDimension._3D)
                        {
                            Vector3D normal = Vector3D.NormalFromPlane((Vector3D)(triangles[i].p1.transformedPosition),
                                                                       (Vector3D)(triangles[i].p2.transformedPosition),
                                                                       (Vector3D)(triangles[i].p3.transformedPosition));
                            if ((normal * (Vector3D)(triangles[i].p1.transformedPosition)).ApproxMoreThanEquals(0)) continue;
                        }

                        // Clip the triangle in view space
                        triangleQueue.Enqueue(triangles[i]);
                        ClipTriangles(triangleQueue, renderingEntity.ViewClippingPlanes, ClipTriangle);
                        if (triangleQueue.Count == 0) continue;

                        for (int j = 0; j < triangleQueue.Count; j++)
                        {
                            Triangle clippedTriangle = triangleQueue.Dequeue();

                            clippedTriangle.p1.viewSpacePosition = clippedTriangle.p1.transformedPosition;
                            clippedTriangle.p2.viewSpacePosition = clippedTriangle.p2.transformedPosition;
                            clippedTriangle.p3.viewSpacePosition = clippedTriangle.p3.transformedPosition;

                            clippedTriangle.TransformTriangleVertices(renderingEntity.viewToScreen);

                            clippedTriangle.p1.invW = 1 / clippedTriangle.p1.transformedPosition.w;
                            clippedTriangle.p2.invW = 1 / clippedTriangle.p2.transformedPosition.w;
                            clippedTriangle.p3.invW = 1 / clippedTriangle.p3.transformedPosition.w;

                            if (renderingEntity is PerspectiveCamera or Spotlight)
                            {
                                clippedTriangle.p1.transformedPosition *= clippedTriangle.p1.invW;
                                clippedTriangle.p2.transformedPosition *= clippedTriangle.p2.invW;
                                clippedTriangle.p3.transformedPosition *= clippedTriangle.p3.invW;

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
                                        List<Triangle> tileClippedTriangles = ClipTriangle2D(clippedTriangle, tile.startY, tile.startX, tile.endX, tile.endY);
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
                ShadowMapInterpolateTriangle(triangle, buffer, onInterpolation, triangle.p1.invW, triangle.p2.invW, triangle.p3.invW);
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
                        InterpolateTriangle(triangle, buffer, onInterpolation, triangle.p1.invW, triangle.p2.invW, triangle.p3.invW);
                        break;
                    case TextureStyle:
                        InterpolateTextureTriangle(triangle, buffer, OnTextureInterpolation, triangle.p1.invW, triangle.p2.invW, triangle.p3.invW);
                        break;
                }
            }

            tile.triangles.Clear();
        });
    }
}