using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;

public class SpriteAtlasManager : Inst<SpriteAtlasManager>
{
    SpriteAtlas spriteAtla = null;
    Dictionary<string, SpriteAtlas> spriteAtlas = new Dictionary<string, SpriteAtlas>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Sprite GetSprite(string name, string spriteAtlasName = "Icons")
    {
        Sprite sprite = null;
        spriteAtlas.TryGetValue(spriteAtlasName, out spriteAtla);
        if (spriteAtla == null)
        {
            //spriteAtla = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/Images/" + spriteAtlasName + "/" + spriteAtlasName + ".spriteatlas");
            spriteAtla = Resources.Load<SpriteAtlas>("Images/" + spriteAtlasName);
            spriteAtlas[spriteAtlasName] = spriteAtla;
        }
        sprite = spriteAtla.GetSprite(name);
        if (sprite == null)
        {
            Debug.LogError("图集里没有：" + name);
        }
        return sprite;
    }
}
