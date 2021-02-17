inlets = 1;
outlets = 1;

var pas = 0.025;

function EulerIntegration(valXt1)
{
	var intergerVal = valXt1*pas;
	outlet(0, intergerVal);
}