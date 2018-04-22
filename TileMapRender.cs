using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 

public class TileMapRender : IRender {

    // Use this for initialization
    void Start () {

	}

    public override void SetRenderIR(float f) {
        DefaultMaterial = GetComponent<TilemapRenderer>().material;

        Material IRMaterial = Instantiate(Resources.Load("InfraRouge") as Material);
        IRMaterial.SetFloat("_IRValue", baseIRValue);
        IRMaterial.SetFloat("_GreyToColor", 0);
        GetComponent<TilemapRenderer>().material = IRMaterial;
    }

    public override IEnumerator ResetRender() {
        Material current = GetComponent<TilemapRenderer>().material;
        for (int i = 0; i < 20; i++) {
            current.SetFloat("_GreyToColor", (float)i / 20f);
            current.SetFloat("_IRValue", i / 20f + baseIRValue * (1 - i / 20f));
            yield return new WaitForSeconds(0.05f);
        }
        GetComponent<TilemapRenderer>().material = DefaultMaterial;
    }
}
