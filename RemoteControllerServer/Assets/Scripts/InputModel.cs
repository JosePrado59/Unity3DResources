using System.Collections;
using System.Collections.Generic;

public class InputModel
{
    public float XAxis { set; get; }
    public float YAxis { set; get; }

    public InputModel()
    {
        XAxis = 0;
        YAxis = 0;
    }

    public InputModel(float xAxis, float yAxis)
    {
        XAxis = xAxis;
        YAxis = yAxis;
    }
}
