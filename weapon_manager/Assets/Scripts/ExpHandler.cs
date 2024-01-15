using UnityEngine;

public class ExpHandler
{
   public int CurrentLevel { get; private set; } = 0;
   public float CurrentExp { get; private set; } = 0;
   public float MaxExp { get; private set; } = 1000;

   public void AddExp(float expVal)
   {
      CurrentExp += expVal;

      if (CurrentExp >= MaxExp)
      {
         Levelup();
      }
   }

   void Levelup(int levelToAdd = 1)
   {
      CurrentLevel += levelToAdd;
      var overflow = Mathf.Abs(CurrentExp - MaxExp);
      CurrentExp = overflow;

      MaxExp *= 1.8f; // magic number, 1.8 because its cool :)
   }

}
