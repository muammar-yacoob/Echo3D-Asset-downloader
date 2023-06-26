using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    [System.Serializable]
    public class ModelInfo
    {
        public GLTFAccessor[] accessors;
        public GLTFAsset asset;
        public GLTFBufferView[] bufferViews;
        public GLTFBuffer[] buffers;
        public GLTFImage[] images;
        public GLTFMaterial[] materials;
        public GLTFMesh[] meshes;
        public GLTFNode[] nodes;
        public GLTFScene[] scenes;
        public GLTFSampler[] samplers;
        public int scene;
        public GLTFTexture[] textures;
    }

    [Serializable]
    public class GLTFAccessor
    {
        public int bufferView;
        public int componentType;
        public int count;
        public float[] max;
        public float[] min;
        public string type;
    }

    [Serializable]
    public class GLTFAsset
    {
        public GLTFExtras extras;
        public string generator;
        public string version;
    }

    [Serializable]
    public class GLTFExtras
    {
        public string author;
        public string license;
        public string source;
        public string title;
    }

    [Serializable]
    public class GLTFBufferView
    {
        public int buffer;
        public int byteLength;
        public int byteOffset;
        public int byteStride;
        public string name;
        public int target;
    }

    [Serializable]
    public class GLTFBuffer
    {
        public int byteLength;
        public string name;
    }

    [Serializable]
    public class GLTFImage
    {
        public int bufferView;
        public string mimeType;
    }

    [Serializable]
    public class GLTFMaterial
    {
        public bool doubleSided;
        public string name;
        public GLTFMaterialPBRMetallicRoughness pbrMetallicRoughness;
    }

    [Serializable]
    public class GLTFMaterialPBRMetallicRoughness
    {
        public GLTFMaterialTexture baseColorTexture;
        public float metallicFactor;
        public float roughnessFactor;
    }

    [Serializable]
    public class GLTFMaterialTexture
    {
        public int index;
    }

    [Serializable]
    public class GLTFMesh
    {
        public string name;
        public GLTFMeshPrimitive[] primitives;
    }

    [Serializable]
    public class GLTFMeshPrimitive
    {
        public Dictionary<string, int> attributes;
        public int indices;
        public int material;
        public int mode;
    }

    [Serializable]
    public class GLTFNode
    {
        public int[] children;
        public float[] matrix;
        public string name;
        public int mesh;
        public float[] rotation;
        public float[] translation;
    }

    [Serializable]
    public class GLTFScene
    {
        public string name;
        public int[] nodes;
    }

    [Serializable]
    public class GLTFSampler
    {
        public int magFilter;
        public int minFilter;
        public int wrapS;
        public int wrapT;
    }

    [Serializable]
    public class GLTFTexture
    {
        public int sampler;
        public int source;
    }
}