﻿using UnityEngine;
using UnityEditor;
using BlazeAISpace;

[CanEditMultipleObjects]
[CustomEditor(typeof(BlazeAI))]

public class BlazeAIEditor : Editor
{
    #region EDITOR VARIABLES

    string[] tabs = {"General", "States", "Vision", "Off Mesh", "Distract", "Hit", "Death", "Companion"};
    int tabSelected = 0;
    int tabIndex = -1;

    string[] generalSubTabs = {"Setup", "Waypoints", "Misc", "Warnings"};
    int generalSubTabSelected = 0;
    int generalSubTabIndex = -1;

    string[] visionSubTabs = {"Setup", "Targets", "Events", "Settings"};
    int visionSubTabSelected = 0;
    int visionSubTabIndex = -1;

    BlazeAI script;

    #endregion

    #region SERIALIZED PROPERTIES

    SerializedProperty useRootMotion,
    groundLayers,

    audioScriptable,
    agentAudio,
    
    waypointsProp,
    waypoints,
    loop,
    waypointsRotation,
    timeBeforeTurning,
    turnSpeed,
    showWaypoints,
    randomize,
    randomizeRadius,
    showRandomizeRadius,
    useMovementTurning,
    movementTurningSensitivity,
    useTurnAnims,
    rightTurnAnimNormal,
    leftTurnAnimNormal,
    rightTurnAnimAlert,
    leftTurnAnimAlert,
    turningAnimT,
    
    checkEnemyContact,
    enemyContactRadius,
    showEnemyContactRadius,
    
    useNormalStateOnAwake,
    normalStateBehaviour,

    useAlertStateOnAwake,
    alertStateBehaviour,

    attackStateBehaviour,
    coverShooterMode,
    coverShooterBehaviour,
    goingToCoverBehaviour,

    useSurprisedState,
    surprisedStateBehaviour,

    visionProp,
    layersToDetect,
    hostileAndAlertLayers,
    hostileTags,
    alertTags,
    visionPosition,
    maxSightLevel,
    checkTargetHeight,
    minSightLevel,
    useMinLevel,
    visionDuringNormalState,
    visionDuringAlertState,
    visionDuringAttackState,
    head,
    showNormalVision,
    showAlertVision,
    showAttackVision,
    showMaxSightLevel,
    showMinSightLevel,
    enemyEnterEvent,
    enemyLeaveEvent,
    pulseRate,
    multiRayVision,
    useVisionMeter,
    visionMeterSpeeds,

    canDistract,
    distractedStateBehaviour,
    priorityLevel,
    turnOnEveryDistraction,
    turnAlertOnDistract,
    playDistractedAudios,

    useHitCooldown,
    maxHitCount,
    hitCooldown,
    hitStateBehaviour,
    
    deathAnim,
    deathAnimT,
    playDeathAudio,
    deathCallRadius,
    agentLayersToDeathCall,
    showDeathCallRadius,
    useRagdoll,
    useNaturalVelocity,
    hipBone,
    deathRagdollForce,
    deathEvent,
    destroyOnDeath,
    timeBeforeDestroy,

    friendly,

    distanceCull,
    animToPlayOnCull,

    ignoreUnreachableEnemy,
    fallBackPoints,
    showPoints,
    
    useOffMeshLinks,
    jumpMethod,
    jumpHeight,
    jumpDuration,
    useMovementSpeedForJump,
    jumpSpeed,
    jumpAnim,
    fallAnim,
    jumpAnimT,
    onTeleportStart,
    onTeleportEnd,

    climbLadders,
    ladderLayers,
    climbUpAnim,
    climbUpSpeed,
    climbToTopAnim,
    climbToTopDuration,
    climbToTopHeadRoom,
    climbAnimT,
    
    warnEmptyBehavioursOnStart,
    warnEmptyAnimations,
    warnEmptyAudio,
    warnAnomaly,

    companionMode,
    companionTo,
    companionBehaviour;

    #endregion

    #region UNITY METHODS

    void OnEnable()
    {
        GetLastTabsSelected();  

        useRootMotion = serializedObject.FindProperty("useRootMotion");
        groundLayers = serializedObject.FindProperty("groundLayers");

        audioScriptable = serializedObject.FindProperty("audioScriptable");
        agentAudio = serializedObject.FindProperty("agentAudio");


        GetWaypointsProperties();
        GetVisionProperties();


        checkEnemyContact = serializedObject.FindProperty("checkEnemyContact");
        enemyContactRadius = serializedObject.FindProperty("enemyContactRadius");
        showEnemyContactRadius = serializedObject.FindProperty("showEnemyContactRadius");


        distanceCull = serializedObject.FindProperty("distanceCull");
        animToPlayOnCull = serializedObject.FindProperty("animToPlayOnCull");


        friendly = serializedObject.FindProperty("friendly");


        ignoreUnreachableEnemy = serializedObject.FindProperty("ignoreUnreachableEnemy");
        fallBackPoints = serializedObject.FindProperty("fallBackPoints");
        showPoints = serializedObject.FindProperty("showPoints");


        useOffMeshLinks = serializedObject.FindProperty("useOffMeshLinks");
        jumpMethod = serializedObject.FindProperty("jumpMethod");
        jumpHeight = serializedObject.FindProperty("jumpHeight");
        jumpDuration = serializedObject.FindProperty("jumpDuration");
        useMovementSpeedForJump = serializedObject.FindProperty("useMovementSpeedForJump");
        jumpSpeed = serializedObject.FindProperty("jumpSpeed");
        jumpAnim = serializedObject.FindProperty("jumpAnim");
        fallAnim = serializedObject.FindProperty("fallAnim");
        jumpAnimT = serializedObject.FindProperty("jumpAnimT");
        onTeleportStart = serializedObject.FindProperty("onTeleportStart");
        onTeleportEnd = serializedObject.FindProperty("onTeleportEnd");

        climbLadders = serializedObject.FindProperty("climbLadders");
        ladderLayers = serializedObject.FindProperty("ladderLayers");
        climbUpAnim = serializedObject.FindProperty("climbUpAnim");
        climbUpSpeed = serializedObject.FindProperty("climbUpSpeed");
        climbToTopAnim = serializedObject.FindProperty("climbToTopAnim");
        climbToTopDuration = serializedObject.FindProperty("climbToTopDuration");
        climbToTopHeadRoom = serializedObject.FindProperty("climbToTopHeadRoom");
        climbAnimT = serializedObject.FindProperty("climbAnimT");

        warnEmptyBehavioursOnStart = serializedObject.FindProperty("warnEmptyBehavioursOnStart");
        warnEmptyAnimations = serializedObject.FindProperty("warnEmptyAnimations");
        warnEmptyAudio = serializedObject.FindProperty("warnEmptyAudio");
        warnAnomaly = serializedObject.FindProperty("warnAnomaly");


        // STATES TAB
        useNormalStateOnAwake = serializedObject.FindProperty("useNormalStateOnAwake");
        normalStateBehaviour = serializedObject.FindProperty("normalStateBehaviour");


        useAlertStateOnAwake = serializedObject.FindProperty("useAlertStateOnAwake");
        alertStateBehaviour = serializedObject.FindProperty("alertStateBehaviour");


        attackStateBehaviour = serializedObject.FindProperty("attackStateBehaviour");
        coverShooterMode = serializedObject.FindProperty("coverShooterMode");
        coverShooterBehaviour = serializedObject.FindProperty("coverShooterBehaviour");
        goingToCoverBehaviour = serializedObject.FindProperty("goingToCoverBehaviour");


        // SURPRISED TAB
        useSurprisedState = serializedObject.FindProperty("useSurprisedState");
        surprisedStateBehaviour = serializedObject.FindProperty("surprisedStateBehaviour");


        // DISTRACT TAB
        canDistract = serializedObject.FindProperty("canDistract");
        distractedStateBehaviour = serializedObject.FindProperty("distractedStateBehaviour");
        priorityLevel = serializedObject.FindProperty("priorityLevel");
        turnOnEveryDistraction = serializedObject.FindProperty("turnOnEveryDistraction");
        turnAlertOnDistract = serializedObject.FindProperty("turnAlertOnDistract");
        playDistractedAudios = serializedObject.FindProperty("playDistractedAudios");


        // HIT TAB
        useHitCooldown = serializedObject.FindProperty("useHitCooldown");
        maxHitCount = serializedObject.FindProperty("maxHitCount");
        hitCooldown = serializedObject.FindProperty("hitCooldown");
        hitStateBehaviour = serializedObject.FindProperty("hitStateBehaviour");


        // DEATH TAB
        deathAnim = serializedObject.FindProperty("deathAnim");
        deathAnimT = serializedObject.FindProperty("deathAnimT");
        playDeathAudio = serializedObject.FindProperty("playDeathAudio");
        deathCallRadius = serializedObject.FindProperty("deathCallRadius");
        agentLayersToDeathCall = serializedObject.FindProperty("agentLayersToDeathCall");
        showDeathCallRadius = serializedObject.FindProperty("showDeathCallRadius");
        deathEvent = serializedObject.FindProperty("deathEvent");
        useRagdoll = serializedObject.FindProperty("useRagdoll");
        useNaturalVelocity = serializedObject.FindProperty("useNaturalVelocity");
        hipBone = serializedObject.FindProperty("hipBone");
        deathRagdollForce = serializedObject.FindProperty("deathRagdollForce");
        destroyOnDeath = serializedObject.FindProperty("destroyOnDeath");
        timeBeforeDestroy = serializedObject.FindProperty("timeBeforeDestroy");


        // COMPANION TAB
        companionMode = serializedObject.FindProperty("companionMode");
        companionTo = serializedObject.FindProperty("companionTo");
        companionBehaviour = serializedObject.FindProperty("companionBehaviour");
    }

    protected virtual void OnSceneGUI()
    {
        if (script == null) return;

        if (script.waypoints.showWaypoints) {
            DrawWaypointHandles();
        }

        if (script.showPoints) {
            DrawFallBackPointsHandles();
        }
    }

    public override void OnInspectorGUI () 
    {
        StyleToolbar();
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Hover on any property below for insights", EditorStyles.helpBox);

        // reset the tabs
        tabIndex = -1;
        generalSubTabIndex = -1;
        visionSubTabIndex = -1;

        script = (BlazeAI)target;
        
        // tab selection
        switch (tabSelected)
        {
            case 0:
                GeneralTab(script);
                break;
            case 1:
                StatesTab(script);
                break;
            case 2:
                VisionTab();
                break;
            case 3:
                OffMeshTab(script);
                break;
            case 4:
                DistractionsTab(script);
                break;
            case 5:
                HitTab(script);
                break;
            case 6:
                DeathTab(script);
                break;
            case 7:
                CompanionTab(script);
                break;
        }

        EditorPrefs.SetInt("BlazeTabSelected", tabSelected);
        EditorPrefs.SetInt("BlazeGeneralSubTabSelected", generalSubTabSelected);
        EditorPrefs.SetInt("BlazeVisionSubTabSelected", visionSubTabSelected);
        serializedObject.ApplyModifiedProperties();
    }

    #endregion

    #region DRAWING INSPECTOR

    void StyleToolbar()
    {   
        GUILayout.BeginHorizontal();
        
        foreach (string item in tabs) 
        {
            tabIndex++;

            if (tabIndex == 4) {
                GUILayout.EndHorizontal();
                EditorGUILayout.Space(0.2f);
                GUILayout.BeginHorizontal();
            }

            if (tabIndex == tabSelected) {
                // selected button
                GUILayout.Button(item, ToolbarStyling(true), GUILayout.MinWidth(80), GUILayout.Height(40));
            }
            else {
                // unselected buttons
                if (GUILayout.Button(item, ToolbarStyling(false), GUILayout.MinWidth(80), GUILayout.Height(40))) {
                    // this will get set when button is pressed
                    tabSelected = tabIndex;
                }
            }
        }

        GUILayout.EndHorizontal();

        // general sub tabs
        if (tabSelected == 0)
        {
            EditorGUILayout.Space(5);
            GUILayout.BeginHorizontal(ToolbarSubTabStyling(false));

            foreach (string subTab in generalSubTabs) 
            {
                generalSubTabIndex++;

                if (generalSubTabIndex == generalSubTabSelected) {
                    GUILayout.Button(subTab, ToolbarSubTabStyling(true), GUILayout.MinWidth(70), GUILayout.Height(25));
                }
                else {
                    if (GUILayout.Button(subTab, ToolbarSubTabStyling(false), GUILayout.MinWidth(70), GUILayout.Height(25))) {
                        generalSubTabSelected = generalSubTabIndex;
                    }
                }
            }

            GUILayout.EndHorizontal();
            return;
        }

        // vision sub tabs
        if (tabSelected == 2)
        {
            EditorGUILayout.Space(5);
            GUILayout.BeginHorizontal(ToolbarSubTabStyling(false));

            foreach (string subTab in visionSubTabs) 
            {
                visionSubTabIndex++;

                if (visionSubTabIndex == visionSubTabSelected) {
                    GUILayout.Button(subTab, ToolbarSubTabStyling(true), GUILayout.MinWidth(70), GUILayout.Height(25));
                }
                else {
                    if (GUILayout.Button(subTab, ToolbarSubTabStyling(false), GUILayout.MinWidth(70), GUILayout.Height(25))) {
                        visionSubTabSelected = visionSubTabIndex;
                    }
                }
            }

            GUILayout.EndHorizontal();
        }
    }

    // render the general tab properties
    void GeneralTab(BlazeAI script)
    {   
        // setup sub tab
        if (generalSubTabSelected == 0) {
            DrawGeneralSetup();
            EditorGUILayout.Space();
            return;
        }

        // waypoints sub tab
        if (generalSubTabSelected == 1) {
            DrawGeneralWaypoints();
            EditorGUILayout.Space();
            return;
        }

        // misc sub tab
        if (generalSubTabSelected == 2) {
            DrawGeneralMisc();
            EditorGUILayout.Space();
            return;
        }

        // warnings sub tab
        if (generalSubTabSelected == 3) {
            DrawGeneralWarnings();
            EditorGUILayout.Space();
        }
    }

    // render the states classes
    void StatesTab(BlazeAI script)
    {
        EditorGUILayout.PropertyField(useNormalStateOnAwake);
        EditorGUILayout.PropertyField(normalStateBehaviour);
        EditorGUILayout.Space(5);

        EditorGUILayout.PropertyField(useAlertStateOnAwake);
        EditorGUILayout.PropertyField(alertStateBehaviour);   
        EditorGUILayout.Space(18);

        EditorGUILayout.LabelField("Attack State", EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(script.coverShooterMode);
            EditorGUILayout.PropertyField(attackStateBehaviour);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.Space(5);

        EditorGUILayout.PropertyField(coverShooterMode);
        EditorGUI.BeginDisabledGroup(!script.coverShooterMode);
            EditorGUILayout.PropertyField(coverShooterBehaviour);
            EditorGUILayout.PropertyField(goingToCoverBehaviour);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.Space(20);

        EditorGUILayout.LabelField("Surprised State is an optional & temporary state that triggers when the AI finds a target in Normal State. You can uncheck the option and delete the added behaviour if not needed.", EditorStyles.helpBox);
        EditorGUILayout.PropertyField(useSurprisedState);
        EditorGUILayout.PropertyField(surprisedStateBehaviour);
        EditorGUILayout.Space(20);
        

        if (GUILayout.Button("Add Behaviours", GUILayout.Height(40))) {
            script.SetPrimeBehaviours();
        }

        EditorGUILayout.Space(5);
    }

    void VisionTab()
    {
        if (visionSubTabSelected == 0) {
            DrawVisionSetup();
            EditorGUILayout.Space();
            return;
        }

        if (visionSubTabSelected == 1) {
            DrawVisionTargets();
            EditorGUILayout.Space();
            return;
        }

        if (visionSubTabSelected == 2) {
            DrawVisionEvents();
            EditorGUILayout.Space();
            return;
        }

        if (visionSubTabSelected == 3) {
            DrawVisionSettings();
            EditorGUILayout.Space();
            return;
        }
    }

    void OffMeshTab(BlazeAI script)
    {
        EditorGUILayout.PropertyField(useOffMeshLinks);

        if (!script.useOffMeshLinks) return;

        
        EditorGUILayout.PropertyField(jumpMethod);
        EditorGUILayout.Space();

        if (script.jumpMethod == BlazeAI.OffMeshLinkJumpMethod.Teleport) {
            EditorGUILayout.PropertyField(onTeleportStart);
            EditorGUILayout.PropertyField(onTeleportEnd);
        }

        if (script.jumpMethod == BlazeAI.OffMeshLinkJumpMethod.Parabola) {
            EditorGUILayout.PropertyField(jumpHeight);
            EditorGUILayout.PropertyField(jumpDuration);
            EditorGUILayout.Space();
        }

        if (script.jumpMethod == BlazeAI.OffMeshLinkJumpMethod.NormalSpeed) {
            EditorGUILayout.PropertyField(useMovementSpeedForJump);

            if (!script.useMovementSpeedForJump) {
                EditorGUILayout.PropertyField(jumpSpeed);
            }

            EditorGUILayout.Space();
        }

        if (script.jumpMethod == BlazeAI.OffMeshLinkJumpMethod.NormalSpeed || 
        script.jumpMethod == BlazeAI.OffMeshLinkJumpMethod.Parabola) {
            EditorGUILayout.PropertyField(jumpAnim);
            EditorGUILayout.PropertyField(fallAnim);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(jumpAnimT);
        }

        EditorGUILayout.Space(10);
        
        EditorGUILayout.PropertyField(climbLadders);
        if (!script.climbLadders) return;

        EditorGUILayout.PropertyField(ladderLayers);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(climbUpAnim);
        EditorGUILayout.PropertyField(climbUpSpeed);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(climbToTopAnim);
        EditorGUILayout.PropertyField(climbToTopDuration);
        EditorGUILayout.PropertyField(climbToTopHeadRoom);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(climbAnimT);
    }

    // render the distractions tab class
    void DistractionsTab(BlazeAI script)
    {
        EditorGUILayout.PropertyField(canDistract);
        EditorGUILayout.PropertyField(distractedStateBehaviour);
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(priorityLevel);
        EditorGUILayout.PropertyField(turnOnEveryDistraction);

        EditorGUILayout.PropertyField(turnAlertOnDistract);
        EditorGUILayout.PropertyField(playDistractedAudios);

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Add Behaviour", GUILayout.Height(40))) {
            script.SetDistractedBehaviour();
        }
        
        EditorGUILayout.Space(5);
    }

    // render the hits tab class
    void HitTab(BlazeAI script)
    {
        EditorGUILayout.PropertyField(useHitCooldown);
        if (script.useHitCooldown) {
            EditorGUILayout.PropertyField(maxHitCount);
            EditorGUILayout.PropertyField(hitCooldown);
            EditorGUILayout.Space(10);
        }

        EditorGUILayout.PropertyField(hitStateBehaviour);
        EditorGUILayout.Space(10);

        if (GUILayout.Button("Add Behaviour", GUILayout.Height(40))) {
            script.SetHitBehaviour();
        }
    }

    // render the death tab class
    void DeathTab(BlazeAI script)
    {
        EditorGUILayout.PropertyField(deathAnim);
        EditorGUILayout.PropertyField(deathAnimT);
        EditorGUILayout.Space();

        
        EditorGUILayout.PropertyField(playDeathAudio);
        EditorGUILayout.Space();

    
        EditorGUILayout.PropertyField(deathCallRadius);
        EditorGUILayout.PropertyField(agentLayersToDeathCall);
        EditorGUILayout.PropertyField(showDeathCallRadius);
        EditorGUILayout.Space();
        

        EditorGUILayout.PropertyField(useRagdoll);
        if (script.useRagdoll) {
            EditorGUILayout.PropertyField(useNaturalVelocity);

            if (!script.useNaturalVelocity) {
                EditorGUILayout.PropertyField(hipBone);
                EditorGUILayout.PropertyField(deathRagdollForce);
            }
        }
        EditorGUILayout.Space();


        EditorGUILayout.PropertyField(deathEvent);


        EditorGUILayout.PropertyField(destroyOnDeath);
        if (script.destroyOnDeath) {
            EditorGUILayout.PropertyField(timeBeforeDestroy);
        }


        EditorGUILayout.Space();
    }

    // render the companion tab
    void CompanionTab(BlazeAI script)
    {
        EditorGUILayout.PropertyField(companionMode);
        EditorGUILayout.PropertyField(companionTo);
        EditorGUILayout.PropertyField(companionBehaviour);

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Add Companion Behaviour", GUILayout.Height(40))) {
            script.SetCompanionBehaviour();
        }
    }

    #endregion

    #region DRAW SUBTABS

    // general sub tabs
    void DrawGeneralSetup()
    {
        EditorGUILayout.PropertyField(useRootMotion);
        EditorGUILayout.PropertyField(groundLayers);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(audioScriptable);
        EditorGUILayout.PropertyField(agentAudio);
    }

    void DrawGeneralWaypoints()
    {
        int space = 10;

        EditorGUI.BeginDisabledGroup(script.waypoints.randomize);
            if (script.waypoints.randomize) {
                EditorGUILayout.Space(space);
                EditorGUILayout.LabelField("Locked (Randomize is enabled)", EditorStyles.helpBox);
            }
            
            EditorGUILayout.PropertyField(waypoints);
            EditorGUILayout.PropertyField(loop);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space(space);

        EditorGUILayout.PropertyField(waypointsRotation);
        EditorGUILayout.PropertyField(timeBeforeTurning);
        EditorGUILayout.PropertyField(turnSpeed);
        EditorGUILayout.Space(space);

        EditorGUILayout.PropertyField(showWaypoints);
        EditorGUILayout.Space(space);

        EditorGUI.BeginDisabledGroup(script.waypoints.loop);
            if (script.waypoints.loop) EditorGUILayout.LabelField("Locked (Loop is enabled)", EditorStyles.helpBox);
            EditorGUILayout.PropertyField(randomize);
            EditorGUILayout.PropertyField(randomizeRadius);
            EditorGUILayout.PropertyField(showRandomizeRadius);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.Space(space);

        EditorGUILayout.PropertyField(useMovementTurning);
        EditorGUILayout.PropertyField(movementTurningSensitivity);
        EditorGUILayout.PropertyField(useTurnAnims);
        EditorGUILayout.Space();

        if (!script.waypoints.useTurnAnims) return; 
        EditorGUILayout.PropertyField(rightTurnAnimNormal);
        EditorGUILayout.PropertyField(leftTurnAnimNormal);
        EditorGUILayout.PropertyField(rightTurnAnimAlert);
        EditorGUILayout.PropertyField(leftTurnAnimAlert);
        EditorGUILayout.PropertyField(turningAnimT);
    }

    void DrawGeneralMisc()
    {
        EditorGUILayout.PropertyField(checkEnemyContact);
        if (script.checkEnemyContact) {
            EditorGUILayout.PropertyField(enemyContactRadius);
            EditorGUILayout.PropertyField(showEnemyContactRadius);
        }
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(friendly);
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(distanceCull);
        if (script.distanceCull) {
            EditorGUILayout.PropertyField(animToPlayOnCull);
        }
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(ignoreUnreachableEnemy);
        if (script.ignoreUnreachableEnemy) {
            EditorGUILayout.PropertyField(fallBackPoints);
            EditorGUILayout.PropertyField(showPoints);
        }
    }

    void DrawGeneralWarnings()
    {
        EditorGUILayout.PropertyField(warnEmptyBehavioursOnStart);
        EditorGUILayout.PropertyField(warnEmptyAnimations);
        EditorGUILayout.PropertyField(warnEmptyAudio);
        EditorGUILayout.PropertyField(warnAnomaly);
    }

    // start of vision sub tabs
    void DrawVisionSetup()
    {
        int space = 10;

        EditorGUILayout.PropertyField(visionPosition);
        EditorGUILayout.PropertyField(maxSightLevel);
        EditorGUILayout.PropertyField(checkTargetHeight);
        EditorGUILayout.PropertyField(useMinLevel);
        EditorGUI.BeginDisabledGroup(!script.vision.useMinLevel);
            EditorGUILayout.PropertyField(minSightLevel);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.Space(space);

        EditorGUILayout.PropertyField(visionDuringNormalState);
        EditorGUILayout.PropertyField(visionDuringAlertState);
        EditorGUILayout.PropertyField(visionDuringAttackState);
        EditorGUILayout.Space(space);

        EditorGUILayout.PropertyField(head);
        EditorGUILayout.Space(space);

        EditorGUILayout.PropertyField(showNormalVision);
        EditorGUILayout.PropertyField(showAlertVision);
        EditorGUILayout.PropertyField(showAttackVision);
        EditorGUILayout.PropertyField(showMaxSightLevel);
        EditorGUILayout.PropertyField(showMinSightLevel);
    }

    void DrawVisionTargets()
    {
        EditorGUILayout.PropertyField(layersToDetect);
        EditorGUILayout.PropertyField(hostileAndAlertLayers);
        EditorGUILayout.PropertyField(hostileTags);
        EditorGUILayout.PropertyField(alertTags);
    }

    void DrawVisionEvents()
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(enemyEnterEvent);
        EditorGUILayout.PropertyField(enemyLeaveEvent);
    }

    void DrawVisionSettings()
    {
        EditorGUILayout.PropertyField(pulseRate);
        EditorGUILayout.PropertyField(multiRayVision);
        EditorGUILayout.PropertyField(useVisionMeter);
        if (script.vision.useVisionMeter) {
            EditorGUILayout.PropertyField(visionMeterSpeeds);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("You can read the vision meter using: blaze.visionMeter - and get the potential enemy using: blaze.potentialEnemyToAttack", EditorStyles.helpBox);
        }
    }

    #endregion

    #region SERIALIZATION
    
    void GetWaypointsProperties()
    {
        waypointsProp = serializedObject.FindProperty("waypoints");

        waypoints = waypointsProp.FindPropertyRelative("waypoints");
        loop = waypointsProp.FindPropertyRelative("loop");

        waypointsRotation = waypointsProp.FindPropertyRelative("waypointsRotation");
        timeBeforeTurning = waypointsProp.FindPropertyRelative("timeBeforeTurning");
        turnSpeed = waypointsProp.FindPropertyRelative("turnSpeed");

        showWaypoints = waypointsProp.FindPropertyRelative("showWaypoints");

        randomize = waypointsProp.FindPropertyRelative("randomize");
        randomizeRadius = waypointsProp.FindPropertyRelative("randomizeRadius");
        showRandomizeRadius = waypointsProp.FindPropertyRelative("showRandomizeRadius");

        useMovementTurning = waypointsProp.FindPropertyRelative("useMovementTurning");
        movementTurningSensitivity = waypointsProp.FindPropertyRelative("movementTurningSensitivity");
        useTurnAnims = waypointsProp.FindPropertyRelative("useTurnAnims");

        rightTurnAnimNormal = waypointsProp.FindPropertyRelative("rightTurnAnimNormal");
        leftTurnAnimNormal = waypointsProp.FindPropertyRelative("leftTurnAnimNormal");
        rightTurnAnimAlert = waypointsProp.FindPropertyRelative("rightTurnAnimAlert");
        leftTurnAnimAlert = waypointsProp.FindPropertyRelative("leftTurnAnimAlert");
        turningAnimT = waypointsProp.FindPropertyRelative("turningAnimT");
    }

    void GetVisionProperties()
    {
        visionProp = serializedObject.FindProperty("vision");

        layersToDetect = visionProp.FindPropertyRelative("layersToDetect");
        hostileAndAlertLayers = visionProp.FindPropertyRelative("hostileAndAlertLayers");
        hostileTags = visionProp.FindPropertyRelative("hostileTags");
        alertTags = visionProp.FindPropertyRelative("alertTags");

        visionPosition = visionProp.FindPropertyRelative("visionPosition");
        maxSightLevel = visionProp.FindPropertyRelative("maxSightLevel");
        checkTargetHeight = visionProp.FindPropertyRelative("checkTargetHeight");
        minSightLevel = visionProp.FindPropertyRelative("minSightLevel");
        useMinLevel = visionProp.FindPropertyRelative("useMinLevel");

        visionDuringNormalState = visionProp.FindPropertyRelative("visionDuringNormalState");
        visionDuringAlertState = visionProp.FindPropertyRelative("visionDuringAlertState");
        visionDuringAttackState = visionProp.FindPropertyRelative("visionDuringAttackState");

        head = visionProp.FindPropertyRelative("head");

        showNormalVision = visionProp.FindPropertyRelative("showNormalVision");
        showAlertVision = visionProp.FindPropertyRelative("showAlertVision");
        showAttackVision = visionProp.FindPropertyRelative("showAttackVision");
        showMaxSightLevel = visionProp.FindPropertyRelative("showMaxSightLevel");
        showMinSightLevel = visionProp.FindPropertyRelative("showMinSightLevel");

        enemyEnterEvent = visionProp.FindPropertyRelative("enemyEnterEvent");
        enemyLeaveEvent = visionProp.FindPropertyRelative("enemyLeaveEvent");

        pulseRate = visionProp.FindPropertyRelative("pulseRate");
        multiRayVision = visionProp.FindPropertyRelative("multiRayVision");

        useVisionMeter = visionProp.FindPropertyRelative("useVisionMeter");
        visionMeterSpeeds = visionProp.FindPropertyRelative("visionMeterSpeeds");
    }

    #endregion

    #region STYLING

    public static GUIStyle ToolbarStyling(bool isSelected)
    {
        var btnStyle = new GUIStyle();
        btnStyle.fontSize = 14;
        btnStyle.margin = new RectOffset(4,4,2,2);
        btnStyle.alignment = TextAnchor.MiddleCenter;

        // selected btn style
        if (isSelected) {
            btnStyle.normal.background = MakeTex(1, 1, new Color(1f, 0.55f, 0));
            btnStyle.normal.textColor = Color.black;
            btnStyle.active.textColor = Color.black;
            return btnStyle;
        }

        // unselected btns style
        btnStyle.normal.background = MakeTex(1, 1, new Color(0.15f, 0.15f, 0.15f));
        btnStyle.active.background = MakeTex(1, 1, new Color(0.1f, 0.1f, 0.1f));
        btnStyle.normal.textColor = new Color(1, 0.5f, 0);
        btnStyle.active.textColor = new Color(1, 0.5f, 0);
        return btnStyle;
    }

    public GUIStyle ToolbarSubTabStyling(bool isSelected)
    {
        var btnStyle = new GUIStyle();
        btnStyle.fontSize = 13;
        btnStyle.margin = new RectOffset(0,0,0,0);
        btnStyle.alignment = TextAnchor.MiddleCenter;

        if (isSelected) {
            btnStyle.normal.background = MakeTex(1, 1, new Color(0.85f, 0.85f, 0.85f));
            btnStyle.normal.textColor = Color.black;
            btnStyle.active.textColor = Color.black;
            return btnStyle;
        }

        btnStyle.normal.background = MakeTex(1, 1, new Color(1f, 0.45f, 0));
        btnStyle.active.background = MakeTex(1, 1, new Color(1f, 0.35f, 0));
        btnStyle.normal.textColor = Color.black;
        btnStyle.active.textColor = Color.black;
        return btnStyle;
    }

    // create texture for buttons
    public static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; ++i) {
            pix[i] = col;
        }

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

    // creates a horizontal line
    void HorizontalLine (Color color) 
    {
        GUIStyle horizontalLine;
        horizontalLine = new GUIStyle();
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
        horizontalLine.margin = new RectOffset(0, 0, 4, 4);
        horizontalLine.fixedHeight = 1;

        var c = GUI.color;
        GUI.color = color;
        GUILayout.Box(GUIContent.none, horizontalLine);
        GUI.color = c;
    }

    #endregion

    #region HANDLES
    
    void DrawWaypointHandles()
    {
        if (!script.waypoints.showWaypoints || script.waypoints.randomize) {
            return;
        }

        int max = script.waypoints.waypoints.Count;

        for (int i=0; i<max; i++) 
        {
            EditorGUI.BeginChangeCheck();

            Vector3 currentWaypoint = script.waypoints.waypoints[i];
            Vector3 newTargetPosition = Handles.PositionHandle(currentWaypoint, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(script, "Change Waypoint");
                script.waypoints.waypoints[i] = newTargetPosition;
            }
        }
    }

    void DrawFallBackPointsHandles()
    {
        if (!script.ignoreUnreachableEnemy) return;
        if (!script.showPoints) return;

        EditorGUI.BeginChangeCheck();

        Vector3 currentPoint;
        int max = script.fallBackPoints.Length;

        for (int i=0; i<max; i++) {
            currentPoint = script.fallBackPoints[i];
            Vector3 newTargetPosition = Handles.PositionHandle(currentPoint, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(script, "Change Fallback Point");
                script.fallBackPoints[i] = newTargetPosition;
            }
        }
    }

    #endregion

    #region MISC

    void GetLastTabsSelected()
    {
        // main blaze tab
        if (EditorPrefs.HasKey("BlazeTabSelected")) {
            tabSelected = EditorPrefs.GetInt("BlazeTabSelected");
        }
        else {
            tabSelected = 0;
        }

        // general sub tabs
        if (EditorPrefs.HasKey("BlazeGeneralSubTabSelected")) {
            generalSubTabSelected = EditorPrefs.GetInt("BlazeGeneralSubTabSelected");
        }
        else {
            generalSubTabSelected = 0;
        }

        // vision sub tabs
        if (EditorPrefs.HasKey("BlazeVisionSubTabSelected")) {
            visionSubTabSelected = EditorPrefs.GetInt("BlazeVisionSubTabSelected");
        }
        else {
            visionSubTabSelected = 0;
        }
    }

    #endregion
}