using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandOptions{
}

public abstract class ScenarioCommandHandler{
	protected ScenarioManager scenarioManager;

	public ScenarioCommandHandler(ScenarioManager scenarioManager){
		this.scenarioManager = scenarioManager;
	}

	public virtual void OnCommand(CommandOptions commandOptions)
    {
    }
}
