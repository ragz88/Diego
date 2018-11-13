using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollingUVs_Layers : MonoBehaviour 
{
	//public int materialIndex = 0;
	public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
	public string textureName = "_MainTex";
    public bool isImage = true;
    public bool reset = false;
    /*public bool useStencilSettings = false;
    public float stencilComp = 8;
    public float stencilID = 0;
    public float stencilOp = 0;*/
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
        if (!reset)
        {
            /*if (useStencilSettings)
            {
                mat.SetFloat("_StencilComp", stencilComp);
                mat.SetFloat("_Stencil", stencilID);
                mat.SetFloat("_StencilOp", stencilOp);
            }*/
            uvOffset += (uvAnimationRate * Time.deltaTime);
            //if( GetComponent<Renderer>().enabled )
            //{
            //GetComponent<Renderer>().sharedMaterial.SetTextureOffset( textureName, uvOffset );
            mat.SetTextureOffset(textureName, uvOffset);
            //}*/
        }
        else
        {
            uvOffset = new Vector2(0,0);
            mat.SetTextureOffset(textureName, uvOffset);
        }
    }
}