using System;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
   public class UIHelper : MonoBehaviour
   {
      [SerializeField] private GameObject gameOverPanel;
      [SerializeField] private Button restartButton;
      public static event Action OnRestartRequested;

      private void OnEnable()
      {
         AddListeners();
      }

      private void OnDisable()
      {
         RemoveListeners();
      }

      private void HandleOnGameOver()
      {
         gameOverPanel.gameObject.SetActive(true);
      }

      private void OnRestartButtonClicked()
      {
         gameOverPanel.gameObject.SetActive(false);
         OnRestartRequested?.Invoke();
      }

      private void AddListeners()
      {
         CustomPhysics.OnBallGrounded += HandleOnGameOver;
         restartButton.onClick.AddListener(OnRestartButtonClicked);
      }

      private void RemoveListeners()
      {
         CustomPhysics.OnBallGrounded -= HandleOnGameOver;
         restartButton.onClick.RemoveListener(OnRestartButtonClicked);
      }
   }
}
