using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.U2D;
using UnityEngine.U2D;

public class CreateSpriteAtlas
{
    static string rootPath = (Application.dataPath + "/").Replace("\\", "/");
    static string pngExtension = ".png";
    static string spineExtension = ".asset";
    static string fontExtension = ".fontsettings";
    static string atlasExtension = ".spriteatlas";
    static string atlasNamePre = "atlas_";
    static string fileSearchPattern = "*.*";
    static int createAtlasCount = 0;
    static int deleteAtlasCount = 0;
    static SpriteAtlasPackingSettings packSet = new SpriteAtlasPackingSettings()
    {
        blockOffset = 1,
        enableRotation = false,
        enableTightPacking = false,
        padding = 2,
    };
    static SpriteAtlasTextureSettings textureSet = new SpriteAtlasTextureSettings()
    {
        readable = false,
        generateMipMaps = false,
        sRGB = true,
        filterMode = FilterMode.Bilinear,
    };
    static TextureImporterPlatformSettings defaultPlatformSet = new TextureImporterPlatformSettings()
    {
        name = "DefaultTexturePlatform",
        format = TextureImporterFormat.Automatic,
    };
    static TextureImporterPlatformSettings standalonePlatformSet = new TextureImporterPlatformSettings()
    {
        name = "Standalone",
        overridden = true,
        format = TextureImporterFormat.RGBA32,
    };
    static TextureImporterPlatformSettings iPhonePlatformSet = new TextureImporterPlatformSettings()
    {
        name = "iPhone",
        overridden = true,
        format = TextureImporterFormat.ASTC_6x6,
    };
    static TextureImporterPlatformSettings androidPlatformSet = new TextureImporterPlatformSettings()
    {
        name = "Android",
        overridden = true,
        format = TextureImporterFormat.ETC2_RGBA8,
    };
    class AtlasInfo
    {
        public string atlasName;
        public string atlasPath;
        public List<string> texturePaths = new List<string>();
    }
    static List<AtlasInfo> atlasInfos = new List<AtlasInfo>();

    [MenuItem("AssetsTools/创建选择文件夹的SpriteAtlas", false, 1)]
    static void AtlasCreate()
    {
        createAtlasCount = 0;
        atlasInfos.Clear();
        Object[] selects = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
        for (int i = 0; i < selects.Length; ++i)
        {
            Object selected = selects[i];
            string path = AssetDatabase.GetAssetPath(selected);
            if (Directory.Exists(path))
            {
                AtlasCreateByFloder(path);
            }
        }
        AtlasCreateByinfo();
        AssetDatabase.Refresh();
        Debug.Log("图集创建完毕！总计：" + createAtlasCount.ToString());
    }

    [MenuItem("AssetsTools/删除选择文件夹的SpriteAtlas", false, 2)]
    static void AtlasDelete()
    {
        deleteAtlasCount = 0;
        Object[] selects = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
        for (int i = 0; i < selects.Length; ++i)
        {
            Object selected = selects[i];
            string path = AssetDatabase.GetAssetPath(selected);
            if (Directory.Exists(path))
            {
                AtlasDeleteByFloder(path);
            }
        }
        AssetDatabase.Refresh();
        Debug.Log("图集删除完毕！总计：" + deleteAtlasCount.ToString());
    }

    static string GetAtlasName(string floderName)
    {
        return atlasNamePre + floderName + atlasExtension;
    }

    static string GetAssetsPath(string path)
    {
        string name = path.Replace("\\", "/");
        name = name.Replace(rootPath, "Assets/");
        Debug.Log("name:::" + name);
        return name;
    }

    static void AtlasCreateByFloder(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        DirectoryInfo[] dirs = dir.GetDirectories();
        for (int i = 0; i < dirs.Length; ++i)
        {
            AtlasCreateByFloder(dirs[i].FullName);
        }

        FileInfo[] files = dir.GetFiles(fileSearchPattern, SearchOption.TopDirectoryOnly);
        List<string> textures = new List<string>();
        for (int i = 0; i < files.Length; ++i)
        {
            FileInfo f = files[i];
            if (f.Extension.Equals(pngExtension))
            {
                string spinePath = f.FullName.Replace(pngExtension, spineExtension);
                string fontPath = f.FullName.Replace(pngExtension, fontExtension);
                if (!File.Exists(spinePath) && !File.Exists(fontPath))
                {
                    textures.Add(f.FullName);
                }
            }
        }
        if (textures.Count > 0)
        {
            string atlasName = GetAtlasName(dir.Name);
            string atlasPath = Path.Combine(dir.FullName, atlasName);
            atlasPath = GetAssetsPath(atlasPath);
            AtlasInfo atlasInfo = new AtlasInfo()
            {
                atlasName = atlasName,
                atlasPath = atlasPath,
                texturePaths = textures,
            };
            atlasInfos.Add(atlasInfo);
        }
    }

    static void AtlasCreateByinfo()
    {
        AtlasInfo[] atlasInfosArr = atlasInfos.ToArray();
        for (int i = 0; i < atlasInfosArr.Length; ++i)
        {
            AtlasInfo atlasInfo = atlasInfosArr[i];
            string atlasPath = atlasInfo.atlasPath;
            string bundleName = string.Empty;
            string bundleVariant = string.Empty;
            if (File.Exists(atlasPath))
            {
                AssetImporter assetImporter = AssetImporter.GetAtPath(atlasPath);
                bundleName = assetImporter.assetBundleName;
                bundleVariant = assetImporter.assetBundleVariant; ;
                File.Delete(atlasPath);
            }
            SpriteAtlas atlas = new SpriteAtlas();
            atlas.SetIncludeInBuild(true);
            atlas.SetPackingSettings(packSet);
            atlas.SetTextureSettings(textureSet);
            atlas.SetPlatformSettings(defaultPlatformSet);
            atlas.SetPlatformSettings(standalonePlatformSet);
            atlas.SetPlatformSettings(iPhonePlatformSet);
            atlas.SetPlatformSettings(androidPlatformSet);

            List<Sprite> sp_list = new List<Sprite>();
            string[] pathArr = atlasInfo.texturePaths.ToArray();
            for (int j = 0; j < pathArr.Length; ++j)
            {
                string tPath = pathArr[j];
                Sprite sp = AssetDatabase.LoadAssetAtPath<Sprite>(GetAssetsPath(tPath));
                if (sp != null)
                {
                    int width = sp.texture.width;
                    int height = sp.texture.height;
                    if (height > 1024 & width > 512)
                    {
                    }
                    else if (width > 1024 & height > 512)
                    {
                    }
                    else if (width <= 2048 & height <= 2048)
                    {
                        sp_list.Add(sp);
                    }
                }
            }

            if (sp_list.Count > 0)
            {
                atlas.Add(sp_list.ToArray());
                string[] test = atlasPath.Split('/');

                string creatPath = Path.Combine("Assets/Art/Scenes/ScenesTwo/atlas/" + test[test.Length - 2] + "/", test[test.Length - 1]);

                if (!Directory.Exists("Assets/Art/Scenes/ScenesTwo/atlas/" + test[test.Length - 2]))
                {
                    Directory.CreateDirectory("Assets/Art/Scenes/ScenesTwo/atlas/" + test[test.Length - 2]);
                }
                AssetDatabase.CreateAsset(atlas, creatPath);
                if (!string.IsNullOrEmpty(bundleName))
                {
                    AssetImporter assetImporter = AssetImporter.GetAtPath(atlasPath);
                    assetImporter.assetBundleName = bundleName;
                    assetImporter.assetBundleVariant = bundleVariant;
                }
                AssetDatabase.SaveAssets();
                createAtlasCount++;
            }
        }
    }

    static void AtlasDeleteByFloder(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        DirectoryInfo[] dirs = dir.GetDirectories();
        for (int i = 0; i < dirs.Length; ++i)
        {
            AtlasDeleteByFloder(dirs[i].FullName);
        }

        FileInfo[] files = dir.GetFiles(fileSearchPattern, SearchOption.TopDirectoryOnly);
        for (int i = 0; i < files.Length; ++i)
        {
            FileInfo f = files[i];
            string atlasName = GetAtlasName(dir.Name);
            if (f.FullName.EndsWith(atlasName))
            {
                File.Delete(f.FullName);
                deleteAtlasCount++;
            }
        }
    }
}


