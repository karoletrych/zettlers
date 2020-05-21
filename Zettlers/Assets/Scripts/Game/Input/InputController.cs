﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace zettlers
{
    static class BuildingSelected
    {
        public static BuildingType BuildingType { get; set; } = BuildingType.WoodcuttersHut;
    }

    public class InputController : MonoBehaviour
    {
        private PlayerCommandBus _playerCommandBus;
        void Start()
        {
            _playerCommandBus = new PlayerCommandBus();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                BuildingSelected.BuildingType = BuildingType.WoodcuttersHut;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                BuildingSelected.BuildingType = BuildingType.ForesterHut;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                BuildingSelected.BuildingType = BuildingType.StonecuttersHut;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                BuildingSelected.BuildingType = BuildingType.MediumResidence;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                BuildingSelected.BuildingType = BuildingType.MediumResidence;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                BuildingSelected.BuildingType = BuildingType.StorageArea;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                GameObject ground = GameObject.Find("Ground_01");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (ground.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
                {
                    transform.position = hit.point;
                    print(
                        "Building type:" + BuildingSelected.BuildingType.Name() +
                        " position:" + hit.point);
                    _playerCommandBus.Post(new BuildBuildingCommand
                    {
                        BuildingType = BuildingSelected.BuildingType,
                        BuildingId = Guid.NewGuid(),
                        Position = new Vector2Int((int)hit.point.x, (int)hit.point.z)
                    });
                    _playerCommandBus.ProcessEvents();
                }
            }
        }
    }
}