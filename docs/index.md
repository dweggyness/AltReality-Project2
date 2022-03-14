
# Escape Rooms!

### You wake up in a bare room with no recollection of how you get here. Suddenly, a phone in the room rings and the voice on the other side of the line tells you an ominous message... Do you try to escape, or do you make yourself comfortable in your new home?

![Screenshot](https://www.imgur.com/RwcQkL9.png)

## Concept

The project was conceptualized as an "Escape Rooms", where instead of escaping just one room, the player has to escape a series of rooms. The mood that the player should feel in our game is a feeling of being 'trapped', and we tried to invoke that feeling through the usage of our room design. The game starts off in a very tiny room, empty save for a telephone and a bed. It is barely enough to walk around a few steps. There is a door, but there is no handle. The user immediately starts looking for a way out as they want to escape this feeling of being trapped.

The rooms were initally designed by drawing the layout of the each room and the respective puzzles that will be present in each. Though, there have been some minor changes to the designs in our final version, mostly due to time constraints such as the missing cameras. The telephone interactable was also changed, as it was too complicated to use a 'Grab' interaction with a phone and having a wire connecting the earpiece to the phone, so we opted for a payphone-ish design instead, with a lever to activate the call.

![Screenshot](https://www.imgur.com/Y6syvDd.png)
![Screenshot](https://www.imgur.com/YJLmtHV.png)
![Screenshot](https://www.imgur.com/LuXrb9m.png)
![Screenshot](https://www.imgur.com/IXRWwZZ.png)

## Working Process
Both me and Shyngys were working on this project together, so we needed a way to collaborate as it didn't make much sense to work on just 1 computer. We opted to do it in 2 separate scenes, and use GitHub to sync our changes. I handled Room A and the Outside, while Shyngys handled Room B and C. When we were each done with our respective scenes, we would then merge it into a single FinalScene. Our process worked pretty well save for some minor hiccups during the merging process such as the scale being off.

## Technical implementation
Jun:

![Screenshot](https://www.imgur.com/ZJdQJKu.png)

_ **Room 1** includes two interactions, the Lever fixed interactable, and the Card grab interactable. As we didn't use hover-state color change to highlight interactions, I tried to use color and sounds as a way to signify interactables in the scene. The phone itself is mostly dark, except for a bright red lever. The card that the user is supposed to pick up is a neon-green card. They stand out in the room they're in, and I was counting on that to make the user realize what they have to do. 


I also used audio as a way of calling attention to objects. The phone in the first room will ring after the first 7~ seconds or so, and I think it does a good job of calling attention to the phone. Once the user picks up the phone by activating the lever, the voice will start playing, and a little light on the phone will light up, as a way of signifying that something is going on, and that they should continue listening. Once the call ends ( audio clip ends ), I turn off the phone light, so as a way to tell the user that they're done here, and its safe for them to continue doing other things without missing anything from the call. 

The CardReader interaction also has a sound in it. I was worried that users might not figure out that the green card goes to the CardReader, but through some playtesting with some friends I found that everyone could figure it out, so I guess the visual contrast I gave to interactable objects worked out. It plays a beep sound once the card is scanned as an immediate response to user stimuli, and the door opens roughly 2 seconds after in order to give a sense of suspense.

Shyngys: 

![Screenshot](https://www.imgur.com/cx7CvOk.png)
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
    
![Screenshot](https://www.imgur.com/YvFa0eV.pngg)
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

Jun:

![Screenshot](https://www.imgur.com/dTJDhfo.png)
- ** Outside ** The outside scene uses the same interaction from Room A, the phone. I wrote the script in a somewhat modular way, so I was able to get the interaction working just by copy pasting the script to the phone outside with only minor changes. On the outside, I wanted to invoke a sense of 'freedom', especially after being trapped inside the rooms. I did so by having a staircase lead up to the outside world from Room C, as once the user begins their ascent, they will see the sky at the top of the staircase and feel like they've defeated the Escape Rooms and have finally escaped.

I then crush that feeling, attempting to create a mini emotional rollercoaster as they can only spot the phone on the outside once they've fully climbed the staircase. Once they climb the staircase, they spot the same phone from Room A, and it begins to ring. The voice sarcastically congratulates the user for escaping, creating a feeling that perhaps the user hasn't fully escaped yet... The scene is also set in a wheat field, after ascending from the Escape Rooms into a little barn/house of sorts. I was aiming at creating a feeling of being in the middle of nowhere, despite being in such elaborately planned escape rooms earlier. I was inspired by Portal 2, as I want it to feel like barely anything happened on the outside, but on the inside ( in the Escape Rooms ), the user had to go through much trials, but now that they're outside there is nothing left to show for all the effort they put in. 

I was also proud of my technique of creating the wheat field, as I wanted it to feel dense, but without lagging the Oculus Quest by having too many PreFabs. I did so by using a technique called 'billboarding', where I baked the 3D textures into a 2D material which I used at the edges of the fields to give off the illusion of depth. I used Photoshop, and screenshoting the 3D textures from different angles and smushing them up into a singular 2D material. There were also less Prefabs on the back of the scene than the front, as the user will likely be paying attention to the front and not the back.


![Screenshot](https://www.imgur.com/jGKRGjT.png)
![Screenshot](https://www.imgur.com/LBnWHGO.png)

## Challenges
- Collaborating in Unity was pretty challenging, perhaps because we used GitHub instead of PlasticSCM. We rolled with GitHub as we were both familiar with the technology, and we didn't want to risk using a new collaboration tool as it opens potential for errors and bugs, and though GitHub worked out, perhaps PlasticSCM might have made collaboration easier in the long run.
- When we were merging the scenes together, we had some difficulty with the scaling, as Shyngys's scene was way bigger than Jun's. Simply scaling down Shyngys's scene did not work 1:1, as Shyngys's scene was designed for that larger scale, so some parts looked rather off in the combined scene. In the future, we will design off a uniform scale, or draw an architectural plan with dimensions before actually working on the scenes.
- Learning when to pivot from a planned feature is something we need to improve on. Both me and Shyngys spent a considerable amount of time researching how to work on a planned feature, only to ditch it when we realized it was going to take too much time that we couldn't afford. One such example was the telephone grab interaction, we spent 2 hours collectively looking up how to do it, messing with LineRenderers and stuff simply because we just wanted to 'stick with the plan'. In reality we should've gave up much earlier when we realized how hard it was going to be, and dropped it before investing that much time into an impossible feature ( for our current level anyway ).

## What we have learned
- Work with scripts, making object public
- Understand how the GetComponent in C# works
- How to make a time delay using StartCouroutine and IEnumerator
- Working together on one project using GitHub
- How to build the games to VR Headset
- How to work with XR VR Interactions
