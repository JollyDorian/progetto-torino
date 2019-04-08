﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using DG.Tweening;

public class AttackBase1 : MonoBehaviour {
    public LifeManager lm;
    public TurnManager turn;
    public int att = 1;
    public PositionTester tank;
    public PositionTester2 tankP2;
    public PositionHealer2 healerP2;
    public PositionUtility2 utilityP2;
    public PositionDealer2 dealerP2;
    public int RangeHzTank;
    public int RangeVtTank;
    public int RangeHzHealer;
    public int RangeVtHealer;
    public bool isAttack;
    public KeyCode attackButton;
    public SelectionController selection;
    public bool isAttRight;
    public bool isAttLeft;
    public bool isAttUp;
    public bool isAttDown;
    public float strength;
    public int vibrato;
    public BaseGrid grid;

    // Use this for initialization
    void Start () {

        selection = FindObjectOfType<SelectionController>();
        tank = FindObjectOfType<PositionTester>();
        tankP2 = FindObjectOfType<PositionTester2>();
        healerP2 = FindObjectOfType<PositionHealer2>();
        utilityP2 = FindObjectOfType<PositionUtility2>();
        dealerP2 = FindObjectOfType<PositionDealer2>();
        lm = FindObjectOfType<LifeManager>();
        turn = FindObjectOfType<TurnManager>();
        isAttack = false;
        vibrato = 10;
        strength = 0.1f;

    }
	
    void Update()
    {

        SetAttackBase();
        RotationAttack();
        StartCoroutine(SetDirectionAttackBase());
        DisactivePrewiewTank();
    }

   /* public void SetRange()
    {

        RangeHzTank = tank.maxRangeHzTankPlayer1 - tankP2.maxRangeHzTankPlayer2;
        RangeVtTank = tank.maxRangeVtTankPlayer1 - tankP2.maxRangeVtTankPlayer2;
        RangeHzHealer = tank.maxRangeHzTankPlayer1 - healerP2.maxRangeHzHealerPlayer2;
        RangeVtHealer = tank.maxRangeVtTankPlayer1 - healerP2.maxRangeVtHealerPlayer2;
        //altre unità

    }*/

    public void SetAttackBase()
    {
        if(Input.GetKeyDown(attackButton) && turn.isTurn == true && isAttack == false && selection.isActiveTank == true)
        {
            isAttack = true;
            
            gameObject.GetComponent<InputController>().enabled = false;

        }
        else if(Input.GetKeyDown(attackButton) && turn.isTurn == true && isAttack == true && selection.isActiveTank == true)
        {
            isAttack = false;
            gameObject.GetComponent<InputController>().enabled = true;
        }
    }

    public void RotationAttack()
    {
        if(isAttack == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.DOLocalRotate(new Vector3(0, 90, 0), 0.2f);
                isAttUp = true;
                isAttDown = false;
                isAttLeft = false;
                isAttRight = false;
                tank.transform.position = grid.GetWorldPosition(tank.x, tank.y);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.DOLocalRotate(new Vector3(0, -90, 0), 0.2f);
                isAttUp = false;
                isAttDown = true;
                isAttLeft = false;
                isAttRight = false;
                tank.transform.position = grid.GetWorldPosition(tank.x, tank.y);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.DOLocalRotate(new Vector3(0, 180, 0), 0.2f);
                isAttUp = false;
                isAttDown = false;
                isAttLeft = false;
                isAttRight = true;
                tank.transform.position = grid.GetWorldPosition(tank.x, tank.y);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f);
                isAttUp = false;
                isAttDown = false;
                isAttLeft = true;
                isAttRight = false;
                tank.transform.position = grid.GetWorldPosition(tank.x, tank.y);
            }
        }
    }

    IEnumerator SetDirectionAttackBase()
    {
        //SetRange();
        //tank destra
        if(Input.GetKeyDown(KeyCode.W) && isAttack == true && isAttUp == true && tank.isUnitEnemie == true)
        {
            if (tank.hit.transform.gameObject.GetComponent<PositionTester2>())
            {
                DamageTankP2();
                tankP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionHealer2>())
            {
                DamageHealerP2();
                healerP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionUtility2>())
            { 
                DamageUtilityP2();
                utilityP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionDealer2>())
            {
                DamageDealerP2();
                dealerP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
        }
      
        //tanke sinistra
        if (Input.GetKeyDown(KeyCode.S)  && isAttack == true && isAttDown == true && tank.isUnitEnemie == true)
        {

            if (tank.hit.transform.gameObject.GetComponent<PositionTester2>())
            {
                DamageTankP2();
                tankP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionHealer2>())
            {
                DamageHealerP2();
                healerP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionUtility2>())
            {
                DamageUtilityP2();
                utilityP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionDealer2>())
            {
                DamageDealerP2();
                dealerP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
        }
      
        //tank sopra
        if (Input.GetKeyDown(KeyCode.A) && isAttack == true && isAttLeft == true && tank.isUnitEnemie == true)
        {
            if (tank.hit.transform.gameObject.GetComponent<PositionTester2>())
            {
                DamageTankP2();
                tankP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionHealer2>())
            {
                DamageHealerP2();
                healerP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionUtility2>())
            {
                DamageUtilityP2();
                utilityP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionDealer2>())
            {
                DamageDealerP2();
                dealerP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }

        }
      
        // healer sotto
        if (Input.GetKeyDown(KeyCode.D)  && isAttack == true && isAttRight == true && tank.isUnitEnemie == true)
        {
            if (tank.hit.transform.gameObject.GetComponent<PositionTester2>())
            {
                DamageTankP2();
                tankP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionHealer2>())
            {
                DamageHealerP2();
                healerP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionUtility2>())
            {
                DamageUtilityP2();
                utilityP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
            else if (tank.hit.transform.gameObject.GetComponent<PositionDealer2>())
            {
                DamageDealerP2();
                dealerP2.transform.DOShakePosition(2f, strength, vibrato);
                yield return new WaitForSeconds(2f);
                turn.isTurn = false;
            }
        }
    }

    //disattivo prewiew attacco/abilità quando finisco turno
    public void DisactivePrewiewTank()
    {
        if (turn.isTurn == false)
        {
            isAttack = false;
        }
    }

    public void DamageTankP2()
    {
        lm.lifeTankPlayer2 -= att;
        isAttack = false;
        gameObject.GetComponent<InputController>().enabled = true;
        selection.isActiveTank = false;
        selection.contSelectionP1 = 0;
       // turn.isTurn = false;
        //tankP2.transform.DOShakePosition(2f, strength, vibrato);
    }

    public void DamageHealerP2()
    {
        lm.lifeHealerPlayer2 -= att;
        isAttack = false;
        gameObject.GetComponent<InputController>().enabled = true;
        selection.isActiveTank = false;
        selection.contSelectionP1 = 0;
       // turn.isTurn = false;
    }

    public void DamageUtilityP2()
    {
        lm.lifeUtilityPlayer2 -= att;
        isAttack = false;
        gameObject.GetComponent<InputController>().enabled = true;
        selection.isActiveTank = false;
        selection.contSelectionP1 = 0;
        // turn.isTurn = false;
        //tankP2.transform.DOShakePosition(2f, strength, vibrato);
    }

    public void DamageDealerP2()
    {
        lm.lifeDealerPlayer2 -= att;
        isAttack = false;
        gameObject.GetComponent<InputController>().enabled = true;
        selection.isActiveTank = false;
        selection.contSelectionP1 = 0;
        // turn.isTurn = false;
    }

}