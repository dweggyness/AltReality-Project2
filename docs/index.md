
# Escape Rooms!

### You wake up in a bare room with no recollection of how you get here. Suddenly, a phone in the room rings and the voice on the other side of the line tells you an ominous message... Do you try to escape, or do you make yourself comfortable in your new home?

## Techincal implementation
Shyngys: 
- **Room 2** includes the position interaction. There are 4 plates and the user has to press on them with a correct path to open the door to the next room. I implemented it by making the plates public in code, so that it would be easier to assign them in the editor. The following code was for the declaration of the path and of the MeshRenederes
  ```
  public MeshRenderer meshRenderer1;
  public MeshRenderer meshRenderer2;
  public MeshRenderer meshRenderer3;
  public MeshRenderer meshRenderer4;



  int[] corectPath = { 1, 2, 3, 4 };
  int current;
  ```
  
    Afterwards, I thought about how to make the plates interactable. I decided to add the box colider to the plates, edit the collider a little bibt, so that the trigger starts when the player is right on the plate. After that, the only thing left to do was to write the script to understand if the user was correct or not.
    
    ---
    ```
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("press1"))
        {
            meshRenderer1.material.color = Color.cyan;
            if (corectPath[current] == 1)
            {
                meshRenderer1.material.color = Color.green;
                current = 1;
            }
            else if(current == 1)
            {
                meshRenderer1.material.color = Color.green;
            }
            else StartCoroutine(ClearPath());

        }
    }
    ```
    The code above is basically a bunch of if/else conditions to understand where the user presses. The current variable is needed to check wherer the user is right now and how correct are they. On incorrect plate, the plates are getting red. However, I wanted to make them gray again, tehrefore I found the function `StartCoroutine(ClearPath());`. THis basically worked in a way that after 3 seconds of being red, it turns gray again. It was a new thing to learn for me, but really helpful in the following parts of the game implemenation. THe function below basically made the whole *magic*.
    ```
    IEnumerator ClearPath()
    {
        meshRenderer1.material.color = Color.red;
        meshRenderer2.material.color = Color.red;
        meshRenderer3.material.color = Color.red;
        meshRenderer4.material.color = Color.red;
        yield return new WaitForSeconds(2);
        meshRenderer1.material.color = Color.gray;
        meshRenderer2.material.color = Color.gray;
        meshRenderer3.material.color = Color.gray;
        meshRenderer4.material.color = Color.gray;
        current = 0;
    }
    ```
    ---
- **Room 3**
  After implementing the room 2, the work with room 3 was a less painful. We decided to implememnt the interaction with the Ray and Number Pad. This tutorial: https://www.youtube.com/watch?v=4tW7XpAiuDg really helped me in the Room 3 implementation. Following the video, I decided to also add the canvas buttons for the Number Pad. I found out that we can add the function for click events on buttons right in the Unity Editor. For the logic, this function below is here:
  ```
   public void OnClick(int t){
        string resnow = "";
        userCode[currentCode] = t;
        currentCode++;

        //writing the code written by the user into the field
        for (int i = 0; i < currentCode; i++){
            resnow = resnow + userCode[i].ToString();
        }
        textField.GetComponent<UnityEngine.UI.Text>().text = resnow;
        if (currentCode == 3){
            if (CheckTwoArrays(userCode, correctCode)){
                StartCoroutine(checkCode(Color.green));
                StartCoroutine(OpenDoor(door2, doorStartPos2, doorEndPos2));
            } else{
                StartCoroutine(checkCode(Color.red));
                currentCode = 0;
                userCode = new int[3];
            }
            
        }
    }
  ```
  Using the numbers that we made in the code that would work, we just checked the current number and when there are 3 numbers written, they are displayed on the text above the Num Pad and correctly or incorrectly. After a correct input, the door will be opened, however on incorrect input, the same logic as in the Room 2 it will daiplsy red and then after 2 seconds it will be empty and gray.


## What we have learned
- Work with scripts, making object public
- Understand how the GetComponent in C# works
- Hwo to make a time delay using StartCouroutine and IEnumerator
- Working together on one project using GitHub
- How to build the games to VR Headset
- How to work with XR VR Interactions
