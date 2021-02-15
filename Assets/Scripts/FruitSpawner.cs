using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class FruitSpawner : MonoBehaviour
{
   private int score;
   private int bestscore;
   private int lifepoint;

   public Text scoreText;
   public Text bestText;
   public Image[] pointsImages;
   public GameObject LosePanel;
   public GameObject fruitPrefab;
   public Transform[] spawnPoints;

   public float minDelay = .1f;
   public float maxDelay = 1f;
   private bool spawnAllow = true;
   private void Start()
   {
      NewGame();
      StartCoroutine(SpawnFruits());
   }

   public void ReloadGame()
   {
      SceneManager.LoadScene("GameScene");
   }

   void NewGame()
   {
      score = 0;
      lifepoint = 3;
      StreamReader sr = new StreamReader("bestResult.txt");
      bestText.text = sr.ReadLine();
      int.TryParse(string.Join("", bestText.text.Where(c => char.IsDigit(c))), out bestscore);
      sr.Close();
   }
   
   public void LoseLife()
   {
      if (lifepoint == 0)
         return;
      lifepoint--;
      pointsImages[lifepoint].enabled = false;
      if (lifepoint == 0)
         Death();
   }

   public void Death()
   {
      LosePanel.SetActive(true);
      spawnAllow = false;
      StreamWriter sw = new StreamWriter("bestResult.txt");
      sw.Write(bestText.text);
      sw.Close();
   }

   IEnumerator SpawnFruits()
   {
      while (spawnAllow)
      {
         float delay = Random.Range(minDelay, maxDelay);
         yield return new WaitForSeconds(1f);

         int spawnIndex = Random.Range(0, spawnPoints.Length);
         Transform spawnPoint = spawnPoints[spawnIndex];
         
         
         GameObject spawnedFruit = Instantiate(fruitPrefab, spawnPoint.position, spawnPoint.rotation);
         Destroy(spawnedFruit,5f);
      }
   }

   public void ScoreIncrement()
   {
      score++;
      scoreText.text = "Score: " + score.ToString();

      if (score > bestscore)
      {
         bestscore = score;
         bestText.text = "The best: " + bestscore.ToString();
      }
   }
}
