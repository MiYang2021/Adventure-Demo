using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicShoot : MonoBehaviour
{
    private GameObject playerUnit;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        playerUnit = GameObject.FindGameObjectWithTag("Player");
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(playerUnit.transform.position.x, playerUnit.transform.position.y + 0.5f, playerUnit.transform.position.z), speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
