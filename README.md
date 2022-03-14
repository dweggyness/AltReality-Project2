# Escape Rooms!

### You wake up in a white room with only a phone on a desk near you. Suddenly, the phone rings and the voice on the other side of the line tells you that you have 10 minutes to leave the building. You see the timer start on the wall and now the only option left is to follow the rules. Will you be able to surpass this challenge or wait to the time to finish?


## Techincal implementation
Shyngys: 
- Room 2 includes the position interaction. There are 4 plates and the user has to press on them with a correct path to open the door to the next room. I implemented it by making the plates public in code, so that it would be easier to assign them in the editor. The following code was for the declaration of the path and of the MeshRenederes
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
