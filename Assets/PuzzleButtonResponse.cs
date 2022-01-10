using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtonResponse : MonoBehaviour
{
    string code = "1321";
    StringBuilder enteredCode = new StringBuilder();
    public GameObject door;
    public List<PuzzleButton> buttons;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Ammo") || collision.collider.tag.Equals("Player") && enteredCode != null)
        {
            if (enteredCode.ToString().Equals(code))
            {
                // Pass
                door.GetComponent<IDoor>().Unlock();
                enteredCode = null; // Closing StringBuilder
            }
            else
            {
                // Resets the entered code. Try again
                AudioManager.instance.PlayOneShot("Error");
                enteredCode.Clear();
            }
        }
    }
    public void Receive(int i)
    {
        if (enteredCode != null)
            enteredCode.Append(i);
    }

    void Enter()
    {
        enteredCode.ToString();
    }
}
