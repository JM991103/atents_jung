using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보드의 배 배치 정보. 총 256까지 가능
/// </summary>
public enum ShipType : byte
{
    None = 0,   // 배가 배치되어 있지 않다.
    Carrier,    // 항공모함이 배치되어 있다. (사이즈 5)
    Battleship, // 전함이 배치되어 있다. (사이즈 4)
    Destoryer,  // 구축함이 배치되어 있다. (사이즈 3)
    Submarine,  // 잠수함이 배치되어 있다. (사이즈 3)
    PatrolBoat  // 경비정이 배치되어 있다. (사이즈 2)
}
