
Welcome to use Easy Effect Controller, this feature is used to support fx artist for creation effects, making the complex effects is simple and easy to control.


"Easy Effect Controller" is a let you can easily organize a timeline effects, can produce effects of some complex combinations in a short time, including some accelerated workflow script to support the fx artists to work effectively.

Features:

- Quickly and easily organize your multiple effects based on a timeline.

- Each special effect with delay time, stop time control.

- Based on "Transform" control, support for "Play on awake" and "Destory on end".

- Support animation detection playback, you can set the animation end to play or stop the effects.

- Attach multiple efficient function scripts, accelerated workflow.

- Dynamic light flicker control, a few parameters can be easily set a dynamic light fade in and fade out.

- Effect scaler control, support Lights, LineRenderer, etc.

- LineRenderer controller, easy to produce similar lighting effects, support a texture sequence.

- Loop player controller, easy to set up special effects loop testing, support the editor mode

- Material texture sequence controller, support Mesh, Particlesystem, TrailRenderer, etc.



Quick Setup:

- To drag the corresponding script to the desired object, you can refer to the usage demo scene.

Hopefully you will find these efficient tools useful in your projects. Good luck!



Version history:

v1.01
-Fixed some inspector ui bugs in Unity4.3 or higher.



Tool Features:

* "EffectController", directory location "../Scripts", can be used to organize your effects play and control each sub object of the start time, end time, easy to use, can replace the cumbersome system key frame animation operation, only need to input the corresponding time value, avoid animation files management cumbersome and error in operation, easier to program call.

   Parameters:
	- "Total Playing Time", total play time, arrive at this time will stop playing all effects, and set the current node is not active state

	- "Play On Awake", if you don't check the selection, set all of the sub-effects for the inactive state, waiting for the program to call.

	- "Destroy On End", if checked, the node is deleted from the current script after the end of the play. If the control sub object is not in the current node, it will be deleted as well.

	- "Use Animation Control" ,if the check, then the contorller is controlled by the corresponding animation.

		- "Set Animation",the animation of required control.

		- "Animating End To Play, or Stop", Set the special effects of the trigger conditions,if the hook is selected after the end of the animation playback effects, do not check the selection after the end of the animation stop effects. 
		Note: The need to set the total playback time is longer than the end of the animation, otherwise it will not trigger

		- "Stopped To Rewind", if you need to loop playback, this setting can make the animation's starting position in the right place.

	- "Effects Count", set the number of control sub objects, based on the Transform information

		- "Effect N", the Transform information of each of the control sub objects, recommended for easy management under the root, if you need to delete on end.

		- "Start Delay Time", setting the delay time of the sub object, the unit is in seconds, the default value is 0, is not greater the total time.

		- "Stop Time", set the playback stop time of the sub object, the default value of 0 means no use, not less than "Delay Time Start", it can not be greater than the total play time




- Tools, directory location "../Scripts/Tools"

	* "EffectLightsFade" can easily generate a lamp effect requires a dynamic lighting simple flash animation, based on code generation for dynamic animation, use the system comes animation mechanism, only in the running game to create animation files, solve each light need to establish the complicated operation of an animation file.

		- This feature depends on dynamic light work, please ensure that the level with a light component and a dynamic light.

		Parameters:
		- "Total Time", the unit is in seconds, animation curve total time.

		- "Fade In Time" is the time to fade in. From the beginning of 0 seconds, at the end of the time, the light intensity value from 0 to the current value of the light.

		- "Fade Out Time" is the time to fade out. From the total time to this time, the total time to reach the end, the lighting value gradually decreased to 0.

		- "Disable On End", if the intensity of the light is changed to 0 eventually, it is recommended to improve the prerformance check, then the light component is disabled.

		- "Loop", if need cycle flashing animation, you can check, at this time "On End Disable" will be invalid.

		- For example: Within 1 second the light changed from the maximum value to 0, TotalTime=1, FadeInTime=0, FadeOutTime=1;
			       Within 1 second the light changed from 0 to the maximum value, TotalTime=1, FadeInTime=1, FadeOutTime=0;
			       Within 3 seconds the light [0->0.5 seconds] changed to the maximum, from [2.5->3 seconds] to 0, TotalTime=3, 
			       FadeOutTime=0.5, FadeInTime=0.5;

			       Fadeout StartTime = TotalTime - FadeOutTime



	* "EffectLoopPlayback" can easily create a loop playback to test your special effects. For some complex effects we may need in the game to run properly preview. Of course, you can also use it to achieve some other simple loop control, based on the active object mechanism.

		- Note that new one object to load this script(loading multiple scripts to control different objects), but please don't put the script into the root nodes or child nodes of the loop object, the loop mechanism can not be self loop of the script.

		- If particle system effects, support previem in editor mode, especially when there are multiple particle systems child nodes, this preview is very useful, so you don't have to each particle system preview cancels selected looping, but also does not appear to cycle out of sync problem.

		Parameters:
		- "Effect Trans Root", the need to loop effects can be dragged here.

		- "Total Playing Time",the cycle time, the unit is in seconds.

		- "Stop Looping", preview in the editor mode or in the game if you want the loop to stop temporarily then check, check again the loop continue to run.

		- "Current Time", the loop time display, we do not need to set this parameter.



	* "EffectScaler", effect scale tool, you can scale effects in the editor mode, and you can remove the script does not affect the final results, and can better support some of the special effects created by this tool set.



	* "LineRenderContrl" is used to make simple Beam effect, support texture UV animation, based on the system of lineRenderer, easily create a Beam effect between the 2 points.

		- This feature depends on LineRenderer, please ensure that the hierarchy contains LineRenderer, if directly drag onto an empty object will automatically create a LineRenderer component.

		- Textures, UV animation only supports horizontal seamless textures, vertical animation sequences, in most cases we use this method to simulate the flow of sequence beam effect between 2 points. This is more to save resources and can achieve better dynamic effects.

		Parameters:
		- "Target", the location of the Beam endpoint, real-time updates, can be moved along with the end position.

		- "Target Offset", simple offset end position to meet the needs of some simple shift.

		- "Tex Mover Total Time", the moving time of the map, in seconds, the shorter the faster moving map, equal to 0 is not moving, it can be considered to control the "speed" of the map.

		- "Tex Sheet Total Time", texture UV animation time, in seconds, meaning that the time required to complete a UV cycle, the same time, the shorter the faster animation.

		- "Tex Sheet Count", number of V directions for the map.

		- "Tex Tiling Vector", map movement direction vector, 1 indicates the positive direction, and -1 indicates the opposite direction.

		- "Tex Sheet Random", the map animation is random.

		- "Tex Auto Distance Div", whether or not to open the map automatic repeat according to distance matching, when the need for dynamic variable-length beam, then will map drawing problems caused, by matching the beam length to repeat mapping is a good way to solve the problem 

		- "Tex Distance Div Value", match distance length unit, each longer than this distance, repeat mapping is incremented by 1, less than the minus 1.



	* "MatTexSheetAnimator", UV mapping animation based material, can support Mesh, TrailRenderer, ParticleSystem, etc.

		Parameters:
		- "Tex Sheet Total Time", UV animation time, in seconds, that the completion of a UV cycle time, the shorter the time the animation more quickly.

		- "Tiles X","Tiles Y", tiles UV settings.

		- "Tex Sheet Random", the map animation is random.

		- "Loop", map animation whether loop, if the loop is not stopped on the last frame.




