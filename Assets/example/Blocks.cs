/*
 * Blocks.cs
 * RVO2 Library C#
 *
 * Copyright (c) 2008-2015 University of North Carolina at Chapel Hill.
 * All rights reserved.
 *
 * Permission to use, copy, modify, and distribute this software and its
 * documentation for educational, research, and non-profit purposes, without
 * fee, and without a written agreement is hereby granted, provided that the
 * above copyright notice, this paragraph, and the following four paragraphs
 * appear in all copies.
 *
 * Permission to incorporate this software into commercial products may be
 * obtained by contacting the authors <geom@cs.unc.edu> or the Office of
 * Technology Development at the University of North Carolina at Chapel Hill
 * <otd@unc.edu>.
 *
 * This software program and documentation are copyrighted by the University of
 * North Carolina at Chapel Hill. The software program and documentation are
 * supplied "as is," without any accompanying services from the University of
 * North Carolina at Chapel Hill or the authors. The University of North
 * Carolina at Chapel Hill and the authors do not warrant that the operation of
 * the program will be uninterrupted or error-free. The end-user understands
 * that the program was developed for research purposes and is advised not to
 * rely exclusively on the program for any reason.
 *
 * IN NO EVENT SHALL THE UNIVERSITY OF NORTH CAROLINA AT CHAPEL HILL OR THE
 * AUTHORS BE LIABLE TO ANY PARTY FOR DIRECT, INDIRECT, SPECIAL, INCIDENTAL, OR
 * CONSEQUENTIAL DAMAGES, INCLUDING LOST PROFITS, ARISING OUT OF THE USE OF THIS
 * SOFTWARE AND ITS DOCUMENTATION, EVEN IF THE UNIVERSITY OF NORTH CAROLINA AT
 * CHAPEL HILL OR THE AUTHORS HAVE BEEN ADVISED OF THE POSSIBILITY OF SUCH
 * DAMAGE.
 *
 * THE UNIVERSITY OF NORTH CAROLINA AT CHAPEL HILL AND THE AUTHORS SPECIFICALLY
 * DISCLAIM ANY WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE AND ANY
 * STATUTORY WARRANTY OF NON-INFRINGEMENT. THE SOFTWARE PROVIDED HEREUNDER IS ON
 * AN "AS IS" BASIS, AND THE UNIVERSITY OF NORTH CAROLINA AT CHAPEL HILL AND THE
 * AUTHORS HAVE NO OBLIGATIONS TO PROVIDE MAINTENANCE, SUPPORT, UPDATES,
 * ENHANCEMENTS, OR MODIFICATIONS.
 *
 * Please send all bug reports to <geom@cs.unc.edu>.
 *
 * The authors may be contacted via:
 *
 * Jur van den Berg, Stephen J. Guy, Jamie Snape, Ming C. Lin, Dinesh Manocha
 * Dept. of Computer Science
 * 201 S. Columbia St.
 * Frederick P. Brooks, Jr. Computer Science Bldg.
 * Chapel Hill, N.C. 27599-3175
 * United States of America
 *
 * <http://gamma.cs.unc.edu/RVO2/>
 */

/*
 * Example file showing a demo with 100 agents split in four groups initially
 * positioned in four corners of the environment. Each agent attempts to move to
 * other side of the environment through a narrow passage generated by four
 * obstacles. There is no roadmap to guide the agents around the obstacles.
 */

#define RVO_OUTPUT_TIME_AND_POSITIONS
#define RVO_SEED_RANDOM_NUMBER_GENERATOR

using System;
using RVO;
using System.Collections.Generic;
using UnityEngine;


class Blocks:MonoBehaviour
{
    /* Store the goals of the agents. */
    IList<Vector2> goals;

    /** Random number generator. */
    System.Random random;

    public GameObject agentPrefab;
    private List<GameObject> _agents;
    private List<GameObject> _blocks;

    void Start()
    {
        goals = new List<Vector2>();
        _agents = new List<GameObject>();
        _blocks = new List<GameObject>();
#if RVO_SEED_RANDOM_NUMBER_GENERATOR
        random = new System.Random();
#else
        random = new Random(0);
#endif
        setupScenario();
    }

    void setupScenario()
    {
        /* Specify the global time step of the simulation. */
        Simulator.Instance.setTimeStep(0.25f);

        /*
            * Specify the default parameters for agents that are subsequently
            * added.
            */
        Simulator.Instance.setAgentDefaults(15.0f, 10, 5.0f, 5.0f, 2.0f, 2.0f, new Vector2(0.0f, 0.0f));

        /*
            * Add agents, specifying their start position, and store their
            * goals on the opposite side of the environment.
            */
        GameObject agent;
        Vector2 pos;
        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                pos = new Vector2(55.0f + i * 10.0f, 55.0f + j * 10.0f);
                Simulator.Instance.addAgent(pos);
                goals.Add(new Vector2(-75.0f, -75.0f));
                agent = (GameObject)GameObject.Instantiate(agentPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                _agents.Add(agent);

                pos = new Vector2(-55.0f - i * 10.0f, 55.0f + j * 10.0f);
                Simulator.Instance.addAgent(pos);
                goals.Add(new Vector2(75.0f, -75.0f));
                agent = (GameObject)GameObject.Instantiate(agentPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                _agents.Add(agent);

                pos = new Vector2(55.0f + i * 10.0f, -55.0f - j * 10.0f);
                Simulator.Instance.addAgent(pos);
                goals.Add(new Vector2(-75.0f, 75.0f));
                agent = (GameObject)GameObject.Instantiate(agentPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                _agents.Add(agent);

                pos = new Vector2(-55.0f - i * 10.0f, -55.0f - j * 10.0f);
                Simulator.Instance.addAgent(pos);
                goals.Add(new Vector2(75.0f, 75.0f));
                agent = (GameObject)GameObject.Instantiate(agentPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                _agents.Add(agent);
            }
        }

        /*
            * Add (polygonal) obstacles, specifying their vertices in
            * counterclockwise order.
            */
        IList<Vector2> obstacle1 = new List<Vector2>();
        obstacle1.Add(new Vector2(-10.0f, 40.0f));
        obstacle1.Add(new Vector2(-40.0f, 40.0f));
        obstacle1.Add(new Vector2(-40.0f, 10.0f));
        obstacle1.Add(new Vector2(-10.0f, 10.0f));
        Simulator.Instance.addObstacle(obstacle1);

        IList<Vector2> obstacle2 = new List<Vector2>();
        obstacle2.Add(new Vector2(10.0f, 40.0f));
        obstacle2.Add(new Vector2(10.0f, 10.0f));
        obstacle2.Add(new Vector2(40.0f, 10.0f));
        obstacle2.Add(new Vector2(40.0f, 40.0f));
        Simulator.Instance.addObstacle(obstacle2);

        IList<Vector2> obstacle3 = new List<Vector2>();
        obstacle3.Add(new Vector2(10.0f, -40.0f));
        obstacle3.Add(new Vector2(40.0f, -40.0f));
        obstacle3.Add(new Vector2(40.0f, -10.0f));
        obstacle3.Add(new Vector2(10.0f, -10.0f));
        Simulator.Instance.addObstacle(obstacle3);

        IList<Vector2> obstacle4 = new List<Vector2>();
        obstacle4.Add(new Vector2(-10.0f, -40.0f));
        obstacle4.Add(new Vector2(-10.0f, -10.0f));
        obstacle4.Add(new Vector2(-40.0f, -10.0f));
        obstacle4.Add(new Vector2(-40.0f, -40.0f));
        Simulator.Instance.addObstacle(obstacle4);

        /*
            * Process the obstacles so that they are accounted for in the
            * simulation.
            */
        Simulator.Instance.processObstacles();
    }

    #if RVO_OUTPUT_TIME_AND_POSITIONS
    void updateVisualization()
    {
        for (int i = 0; i < _agents.Count; ++i)
        {
            Vector2 pos = Simulator.Instance.getAgentPosition(i);
            GameObject agent = _agents[i];
            agent.transform.position = new Vector3(pos.x, pos.y, 0);
        }
    }
    #endif

    void setPreferredVelocities()
    {
        /*
            * Set the preferred velocity to be a vector of unit magnitude
            * (speed) in the direction of the goal.
            */
        for (int i = 0; i < Simulator.Instance.getNumAgents(); ++i)
        {
            Vector2 goalVector = goals[i] - Simulator.Instance.getAgentPosition(i);

            if (RVOMath.absSq(goalVector) > 1.0f)
            {
                goalVector = RVOMath.normalize(goalVector);
            }

            Simulator.Instance.setAgentPrefVelocity(i, goalVector);

            /* Perturb a little to avoid deadlocks due to perfect symmetry. */
            float angle = (float)random.NextDouble() * 2.0f * (float)Math.PI;
            float dist = (float)random.NextDouble() * 0.0001f;

            Simulator.Instance.setAgentPrefVelocity(i, Simulator.Instance.getAgentPrefVelocity(i) +
                dist * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
        }
    }

    bool reachedGoal()
    {
        /* Check if all agents have reached their goals. */
        for (int i = 0; i < Simulator.Instance.getNumAgents(); ++i)
        {
            if (RVOMath.absSq(Simulator.Instance.getAgentPosition(i) - goals[i]) > 400.0f)
            {
                return false;
            }
        }

        return true;
    }

    void Update()
    {

        /* Perform (and manipulate) the simulation. */
        if (!reachedGoal())
        {
            #if RVO_OUTPUT_TIME_AND_POSITIONS
            updateVisualization();
            #endif
            setPreferredVelocities();
            Simulator.Instance.doStep();
        }
    }
}
