using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    public class InputSystem : SystemBase
    {
        private BuildingType BuildingSelected = BuildingType.ForesterHut;
        public IPlayerCommand Command;
        
        protected override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                BuildingSelected = BuildingType.WoodcuttersHut;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                BuildingSelected = BuildingType.ForesterHut;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                BuildingSelected = BuildingType.StonecuttersHut;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                BuildingSelected = BuildingType.MediumResidence;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                BuildingSelected = BuildingType.MediumResidence;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                BuildingSelected = BuildingType.StorageArea;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                GameObject ground = GameObject.Find("Ground_01");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (ground.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
                {
                    Debug.Log(
                        "Building type:" + BuildingSelected.Name() +
                        " position:" + hit.point);
                    Command = new BuildBuildingCommand
                    {
                        BuildingType = BuildingSelected,
                        BuildingId = Guid.NewGuid(),
                        Position = hit.point.ToInt2()
                    };
                }
            }
        }
    }
}
