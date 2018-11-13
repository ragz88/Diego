using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollingUVs_Layers : MonoBehaviour 
{
	//public int materialIndex = 0;
	public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
	public string textureName = "_MainTex";
    public bool isImage = true;
    Material mat;
	
	Vector2 uvOffset = Vector2.zero;

    private void Start()
    {
       if (isImage)
       {
            mat = GetComponent<Image>().material;
       }
       else
       {
            mat = GetComponent<Renderer>().material;
       }
    }

    void LateUpdate() 
	{
		uvOffset += ( uvAnimationRate * Time.deltaTime );
		//if( GetComponent<Renderer>().enabled )
		//{
	    //GetComponent<Renderer>().sharedMaterial.SetTextureOffset( textureName, uvOffset );
        mat.SetTextureOffset(textureName, uvOffset);
        //}*/
    }
}