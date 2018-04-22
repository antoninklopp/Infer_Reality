using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRender : MonoBehaviour{

    public float baseIRValue;

    protected Material DefaultMaterial;

    public virtual IEnumerator ResetRender() {
        Material current = GetComponent<SpriteRenderer>().material; 
        for (int i = 0; i < 20; i++) {
            current.SetFloat("_GreyToColor", i/20f);
            current.SetFloat("_IRValue", i / 20f + baseIRValue * (1 - i / 20f));
            yield return new WaitForSeconds(0.05f); 
        }
        GetComponent<SpriteRenderer>().material = DefaultMaterial;
    }

    public virtual void SetRenderIR(float f) {
        DefaultMaterial = GetComponent<SpriteRenderer>().material;

        Material IRMaterial = Instantiate(Resources.Load("InfraRouge") as Material);
        IRMaterial.SetFloat("_IRValue", baseIRValue);
        IRMaterial.SetFloat("_GreyToColor", 0);
        GetComponent<SpriteRenderer>().material = IRMaterial;
    }

    public void SetRenderIR() {
        SetRenderIR(baseIRValue);
    }

}
