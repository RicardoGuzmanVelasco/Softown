using TMPro;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class Tooltip : MonoBehaviour
    {
        public void Hover(Building theBuilding)
        {
            GetComponentInChildren<TMP_Text>().text = theBuilding.Blueprint.BuildingName;
        }
    }
}