using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Managers
{
    public class LetterManager : MonoBehaviour
    {
        [SerializeField] private Transform letterHolder;
        private GameObject currentLetterPrefab; // Şu anda letterHolder'da bulunan prefabı tutmak için

        [Serializable]
        public class LetterEntry
        {
            public int index;
            public GameObject prefab;
        }

        [SerializeField]
        private List<LetterEntry> letterList = new List<LetterEntry>();

        // Örnek bir mektup eklemek için kullanılabilir
        internal void AddLetter(int index, GameObject prefab)
        {
            // Önce mevcut prefabı temizle
            ClearCurrentLetter();

            // Yeni mektup ekleyerek letterList'e kaydet
            LetterEntry newEntry = new LetterEntry
            {
                index = index,
                prefab = prefab
            };
            letterList.Add(newEntry);

            // Yeni prefabı letterHolder'ın altına instantiate et
            currentLetterPrefab = Instantiate(prefab, letterHolder);
        }

        internal GameObject GetLetterPrefab(int index)
        {
            foreach (var entry in letterList)
            {
                if (entry.index == index)
                {
                    // Letter prefabını instantiate edip letterHolder'ın altına yerleştir
                    ClearCurrentLetter(); // Önce mevcut prefabı temizle
                    currentLetterPrefab = Instantiate(entry.prefab, letterHolder);
                    return currentLetterPrefab;
                }
            }
            return null; // İlgili indekste mektup bulunamadıysa null dönebilir
        }

        // Liste boyutunu döndürmek için kullanılabilir
        public int GetLetterCount()
        {
            return letterList.Count;
        }

        // Liste içeriğini temizlemek için kullanılabilir
        internal void ClearLetters()
        {
            letterList.Clear();
            ClearCurrentLetter(); // letterHolder'daki mevcut prefabı temizle
        }

        // letterHolder'daki mevcut prefabı temizlemek için
        private void ClearCurrentLetter()
        {
            if (currentLetterPrefab != null)
            {
                Destroy(currentLetterPrefab); // Mevcut prefabı sil
                currentLetterPrefab = null;
            }
        }
    }
}
