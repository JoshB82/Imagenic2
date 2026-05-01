using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Loaders;

public partial class OBJLoader
{
    private Dictionary<string, Material> ObtainMaterials()
    {
        return ParseMTLFiles().ToDictionary(
            kvp => kvp.Key,
            kvp => MTLToMaterial(kvp.Value)
        );
    }

    private Dictionary<string, MTL> ParseMTLFiles()
    {
        var mtlDictionary = new Dictionary<string, MTL>();

        foreach (IEnumerable<string> fileData in mtlData)
        {
            string currentName = null;
            MTL currentMTL = null;

            foreach (string line in fileData)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts[0] == "newmtl") // New material
                {
                    if (currentMTL is not null) mtlDictionary.Add(currentName, currentMTL);

                    currentName = parts[1];
                    currentMTL = new MTL();

                    continue;
                }

                if (currentMTL is null) throw new InvalidOperationException("No valid material defined.");

                switch (parts[0])
                {
                    case "#": // Comment
                        break;
                    case "Ka": // Ambient colour
                        float r = float.Parse(parts[1]);
                        float g = float.Parse(parts[2]);
                        float b = float.Parse(parts[3]);

                        ThrowIfNotWithinRange(r, 0, 1);
                        ThrowIfNotWithinRange(g, 0, 1);
                        ThrowIfNotWithinRange(b, 0, 1);

                        currentMTL.AmbientColour = new Vector3D(r, g, b);
                        break;
                    case "Kd": // Diffuse colour
                        r = float.Parse(parts[1]);
                        g = float.Parse(parts[2]);
                        b = float.Parse(parts[3]);

                        ThrowIfNotWithinRange(r, 0, 1);
                        ThrowIfNotWithinRange(g, 0, 1);
                        ThrowIfNotWithinRange(b, 0, 1);

                        currentMTL.DiffuseColour = new Vector3D(r, g, b);
                        break;
                    case "Ks": // Specular colour
                        r = float.Parse(parts[1]);
                        g = float.Parse(parts[2]);
                        b = float.Parse(parts[3]);

                        ThrowIfNotWithinRange(r, 0, 1);
                        ThrowIfNotWithinRange(g, 0, 1);
                        ThrowIfNotWithinRange(b, 0, 1);

                        currentMTL.SpecularColour = new Vector3D(r, g, b);
                        break;
                    case "Ns": // Specular exponent
                        float specularExponent = float.Parse(parts[1]);

                        ThrowIfNotWithinRange(specularExponent, 0, 1000);

                        currentMTL.SpecularExponent = specularExponent;
                        break;
                    case "d": // Dissolve
                        float dissolve = float.Parse(parts[1]);

                        currentMTL.Dissolve = dissolve;
                        break;
                    case "Tr":
                        dissolve = 1 - float.Parse(parts[1]);

                        currentMTL.Dissolve = dissolve;
                        break;
                    case "Tf": // Transmission filter colour
                        switch (parts[1])
                        {
                            case "xyz":
                                float x = float.Parse(parts[2]);
                                float y = float.Parse(parts[3]);
                                float z = float.Parse(parts[4]);
                                // ...
                                break;
                            case "spectral":
                                break;
                            default:
                                r = float.Parse(parts[1]);
                                g = float.Parse(parts[2]);
                                b = float.Parse(parts[3]);

                                ThrowIfNotWithinRange(r, 0, 1);
                                ThrowIfNotWithinRange(g, 0, 1);
                                ThrowIfNotWithinRange(b, 0, 1);

                                currentMTL.TransmissionFilterColour = new Vector3D(r, g, b);
                                break;
                        }
                        break;
                    case "Ni": // Index of refraction
                        float iof = float.Parse(parts[1]);

                        ThrowIfNotWithinRange(iof, 0.001f, 10);

                        currentMTL.IndexOfRefraction = iof;
                        break;
                    case "illum": // Illumination model
                        int illuminationModel = int.Parse(parts[1]);

                        ThrowIfNotWithinRange(illuminationModel, 0, 10);

                        currentMTL.IlluminationModel = illuminationModel;
                        break;
                }
            }

            if (currentMTL is not null) mtlDictionary.Add(currentName, currentMTL);
        }

        return mtlDictionary;
    }

    private class MTL
    {
        public Vector3D AmbientColour { get; set; }
        public Vector3D DiffuseColour { get; set; }
        public Vector3D SpecularColour { get; set; }
        public float SpecularExponent { get; set; }
        public float Dissolve { get; set; }
        public Vector3D TransmissionFilterColour { get; set; }
        public float IndexOfRefraction { get; set; }
        public int IlluminationModel { get; set; }
    }
    
    private static Material MTLToMaterial(MTL mtl)
    {
        return new Material()
        {
            RefractiveIndex = mtl.IndexOfRefraction,
            Transparency = mtl.Dissolve
        };
    }
}