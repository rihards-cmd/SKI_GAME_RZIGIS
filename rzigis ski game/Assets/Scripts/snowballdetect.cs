using UnityEngine;

public class snowballdetect : MonoBehaviour
{
    public string slowballTag;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(slowballTag))
        {
            Destroy(collision.gameObject);
        }
    }

}
