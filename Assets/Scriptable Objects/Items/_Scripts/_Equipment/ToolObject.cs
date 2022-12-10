using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    Mining,
    Cutting,
    Gathering
}

public class ToolObject : EquipmentObject
{
    public ToolType toolType;
}
