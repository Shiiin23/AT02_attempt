using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public float interactionDistance;
    public GameObject intText;
    public GameObject intText2;
    public string doorOpenAnimName, doorCloseAnimName;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject.tag == "door")
            {
                GameObject doorParent = hit.collider.transform.root.gameObject;
                Animator doorAnim = doorParent.GetComponent<Animator>();
                //if door is open, show text that you can close the door.
                if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimName))
                {
                    intText2.SetActive(true);
                    intText.SetActive(false);
                }
                //if door is closed, show text that you can open the door.
                if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimName))
                {
                    intText.SetActive(true);
                    intText2.SetActive(false);
                }

                //When "E" is pressed, open or close the door depending on the state of the door.
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimName))
                    {
                        doorAnim.ResetTrigger("open");
                        doorAnim.SetTrigger("close");

                    }
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimName))
                    {
                        doorAnim.ResetTrigger("close");
                        doorAnim.SetTrigger("open");

                    }
                }
            }
            else
            {
                intText2.SetActive(false);
                intText.SetActive(false);
            }

        }
    }
}
