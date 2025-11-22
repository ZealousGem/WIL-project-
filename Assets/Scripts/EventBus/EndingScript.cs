using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  bool inBox3 = false;

  public string SceneName;

[SerializeField]
    GameObject Interact;

     

     void OnTriggerEnter(Collider other)
    {
        
            if (other.CompareTag("Player"))
            {
                Interact.SetActive(true);

                inBox3 = true;
            }
        

    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Interact.SetActive(false);
            inBox3 = false;
        }
    }

    // Update is called once per frame
   void Update()
    {
          
            if (inBox3)
            {
             
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(SceneName);
                }
            }
        
    }

   
}
