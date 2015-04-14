using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    Vector3 startPos;
    void Start() {

        startPos = transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
       
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - 50f, transform.position.z), Time.deltaTime * 10);
        StartCoroutine("Close");
    }

    IEnumerator Close()
    {
        Debug.Log("boompedS");
        yield return new WaitForSeconds(5f);

        transform.position = startPos;


    }
}
