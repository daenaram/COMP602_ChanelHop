using System.Collections;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    // Apply saved data to the player when the scene starts
    private void Start()
    {
        Time.timeScale = 1f; // Ensure time is normal speed
        if (SaveController.SaveDataToApply != null)
        {
            StartCoroutine(ApplySaveData());
        }
    }

    private IEnumerator ApplySaveData()
    {
        yield return null; // Wait one frame so all PlayerWeaponController.Start() run

        SaveData data = SaveController.SaveDataToApply;

        GameObject player1 = GameObject.FindWithTag("Player1");
        if (player1 != null)
        {
            var pc = player1.GetComponent<PlayerWeaponController>();
            player1.transform.position = data.playerPosition1;
            player1.GetComponent<Health>().SetHealth(data.currentHealth1);
            pc.EquipWeapon(data.currentWeapon1, null);
            player1.GetComponent<PlayerRespawn>().SetCheckpointPosition(data.checkpointPosition1);
            
        }

        GameObject player2 = GameObject.FindWithTag("Player2");
        if (player2 != null)
        {
            var pc = player2.GetComponent<PlayerWeaponController>();
            player2.transform.position = data.playerPosition2;
            player2.GetComponent<Health>().SetHealth(data.currentHealth2);
            pc.EquipWeapon(data.currentWeapon2, null);
            player2.GetComponent<PlayerRespawn>().SetCheckpointPosition(data.checkpointPosition2);
            
        }

        SaveController.SaveDataToApply = null;
    }

}
