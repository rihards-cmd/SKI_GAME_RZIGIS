using UnityEngine;

public class SlalomFlag : MonoBehaviour
{
    public static event GameManager.TimerEvent RacePenalty;

    
    private bool flagPassed = false;
    private enum Direction {Left,Right};
    [SerializeField] private Direction direction;
    [SerializeField] Material goodMat, badMat;
    
    void Update()
    {
       if (PlayerControl.player != null && PlayerControl.player.position.z <transform.position.z && flagPassed == false)
       {
           flagPassed = true;

           Direction passingDirection = Direction.Right;
           if (PlayerControl.player.position.x< transform.position.x)
               passingDirection = Direction.Left;
           Debug.Log("flag passed" +  passingDirection);
           MeshRenderer renderer = GetComponent<MeshRenderer>();
           if (passingDirection == direction)
           {
               Debug.Log("flag passed correctly");
               renderer.material = goodMat;
           }
           else
           {
               Debug.Log("flag passed incorrectly");
               renderer.material = badMat;
               RacePenalty.Invoke();
           }
       }
    }
}
