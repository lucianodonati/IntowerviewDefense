using System.Collections;
using UnityEngine;

namespace Tools
{
    public class DestroyAfterN : MonoBehaviour
    {
        public float seconds = 1;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}