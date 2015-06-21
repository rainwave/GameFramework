﻿using System;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    public Transform m_cacheTransform;

    protected void Awake()
    {
        m_cacheTransform = this.transform;
    }

    protected void Start()
    {
        initAI();
        initAni();
    }

    protected void Update()
    {
        updateBT();
        updateHP();
        updateAni();
    }
}

