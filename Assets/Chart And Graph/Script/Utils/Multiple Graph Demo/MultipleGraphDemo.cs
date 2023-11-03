#define Graph_And_Chart_PRO
using UnityEngine;
using System.Collections;
using ChartAndGraph;
using System.Collections.Generic;

public class MultipleGraphDemo : MonoBehaviour
{
    public GraphChart Graph;
  //  public GraphAnimation Animation;
    public int TotalPoints = 5;

    void Start()
    {
        if (Graph == null) // the ChartGraph info is obtained via the inspector
            return;

        List<DoubleVector2> animationPoints = new List<DoubleVector2>();
        float x = 0f;
        float y = 0f;
        Graph.HorizontalValueToStringMap.Add(10, "Ten");
        Graph.VerticalValueToStringMap.Add(100, "$$");
        Graph.DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
        Graph.DataSource.ClearCategory("SW data"); // clear the "Player 2" category. this category is defined using the GraphChart inspector


        Graph.DataSource.AddPointToCategory("OX data", 0, 0); // each time we call AddPointToCategory 
        Graph.DataSource.AddPointToCategory("SW data", 0, 0); // each time we call AddPointToCategory 

        Graph.DataSource.AddPointToCategory("OX data", 5, 7); // each time we call AddPointToCategory 
        Graph.DataSource.AddPointToCategory("SW data", 5, 3); // each time we call AddPointToCategory 

        Graph.DataSource.AddPointToCategory("OX data", 10, 2); // each time we call AddPointToCategory 
        Graph.DataSource.AddPointToCategory("SW data", 10, 9); // each time we call AddPointToCategory 


        //for (int i = 0; i < TotalPoints; i++)  //add random points to the graph
        //{
        //    Graph.DataSource.AddPointToCategory("OX data", 0, 0); // each time we call AddPointToCategory 
        //    Graph.DataSource.AddPointToCategory("SW data", 0, 0); // each time we call AddPointToCategory 

        //    Graph.DataSource.AddPointToCategory("OX data", 5, 7); // each time we call AddPointToCategory 
        //    Graph.DataSource.AddPointToCategory("SW data", 5, 3); // each time we call AddPointToCategory 

        //    Graph.DataSource.AddPointToCategory("OX data", 10, 2); // each time we call AddPointToCategory 
        //    Graph.DataSource.AddPointToCategory("SW data", 10, 9); // each time we call AddPointToCategory 



        //    //Graph.DataSource.AddPointToCategory("OX data", x, y+10); // each time we call AddPointToCategory 
        //    //Graph.DataSource.AddPointToCategory("SW data", x, y); // each time we call AddPointToCategory 
        //    //Graph.DataSource.AddPointToCategory("Player 2", System.DateTime.Now + System.TimeSpan.FromDays(x), Random.value * 20f + 10f); // each time we call AddPointToCategory 
        //    //animationPoints.Add(new DoubleVector2(ChartDateUtility.DateToValue(System.DateTime.Now + System.TimeSpan.FromDays(x)), Random.value * 10f));
        //    x += 1; 
        //    y += 50;
        //}

        Graph.DataSource.EndBatch(); // finally we call EndBatch , this will cause the GraphChart to redraw itself
        //if (Animation != null)
        //{
            //if (Graph.DataSource.HasCategory("Player 2"))
            //    Animation.Animate("Player 2", animationPoints, 6f);
        //}
    }
}
