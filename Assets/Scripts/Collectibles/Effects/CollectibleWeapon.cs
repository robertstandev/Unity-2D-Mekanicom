using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleWeapon : MonoBehaviour
{
    [SerializeField]private float duration = 8f;
    [SerializeField]private GameObject weaponEffectPrefab;
    private List<GameObject> instantiatedWeaponEffect = new List<GameObject>();
    private GetCharactersFromScene getCharactersFromSceneScript;

    private void Awake() { this.getCharactersFromSceneScript = GetComponent<GetCharactersFromScene>(); }
    private void Start() { instantiateWeaponEffects(); }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(this.getCharactersFromSceneScript.isCollidedObjectInList(other.gameObject))
        {
            this.instantiatedWeaponEffect[getCharactersFromSceneScript.getIndexOfCollidedObject(other.gameObject)].SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
    private void instantiateWeaponEffects()
    {
        for(int i = 0 ; i < this.getCharactersFromSceneScript.getListOfCharactersFromScene().Count; i++)
        {
            this.instantiatedWeaponEffect.Add(Instantiate(this.weaponEffectPrefab , Vector3.zero , Quaternion.identity));
            this.instantiatedWeaponEffect[i].transform.parent = getCharactersFromSceneScript.getListOfCharactersFromScene()[i].transform;
            this.instantiatedWeaponEffect[i].transform.localPosition = Vector3.zero;
            this.instantiatedWeaponEffect[i].GetComponent<AutoHide>().setDuration(this.duration);
        }
    }
}