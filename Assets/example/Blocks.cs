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


using System;
using RVO;
using System.Collections.Generic;
using UnityEngine;


class Blocks:MonoBehaviour
{


    public GameObject agentPrefab;
    private List<GameObject> _agents;
    private List<GameObject> _blocks;

    void Start()
    {
        setupScenario();
    }

    void setupScenario()
    {

       
        GameObject agent;
        Vector3 pos;
        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                pos = new Vector3(55.0f + i * 10.0f, 0, 55.0f + j * 10.0f);
                agent = (GameObject)GameObject.Instantiate(agentPrefab, pos, Quaternion.identity);
                AgentComponent agentComponent = agent.GetComponent<AgentComponent>();
                agentComponent.target = new Vector3(-75.0f, 0, -75.0f);

                pos = new Vector3(-55.0f - i * 10.0f, 0, 55.0f + j * 10.0f);
                agent = (GameObject)GameObject.Instantiate(agentPrefab, pos, Quaternion.identity);
                agentComponent = agent.GetComponent<AgentComponent>();
                agentComponent.target = new Vector3(75.0f, 0, -75.0f);

                pos = new Vector3(55.0f + i * 10.0f, 0, -55.0f - j * 10.0f);
                agent = (GameObject)GameObject.Instantiate(agentPrefab, pos, Quaternion.identity);
                agentComponent = agent.GetComponent<AgentComponent>();
                agentComponent.target = new Vector3(-75.0f, 0, 75.0f);

                pos = new Vector3(-55.0f - i * 10.0f, 0, -55.0f - j * 10.0f);
                agent = (GameObject)GameObject.Instantiate(agentPrefab, pos, Quaternion.identity);
                agentComponent = agent.GetComponent<AgentComponent>();
                agentComponent.target = new Vector3(75.0f, 0, 75.0f);
            }
        }
    }
}

