using Godot;
using System;
using System.Linq;

public partial class drawer : Node2D
{
	Label fpslabel;
	ColorRect sin;
	ColorRect xline;
	ColorRect yline;
	Line2D sinline;

	Timer drawtimer;
	Slider freqslider;
	Slider ampslider;

	float AMP = 100f;
	float freq = 2*Mathf.Pi;
	float linespace = 31.4f;
	int length = 100;

	void drawgraph(){
		 if (drawtimer.IsStopped()){
			drawtimer.Start();
			freq = (float)freqslider.Value;
			AMP = (float)ampslider.Value;
			foreach (ColorRect dot in xline.GetChildren() ){
				dot.QueueFree();
			}
			sinline.ClearPoints();
			//points that connect to form the wave
			for (float x = 0; x<( (3.2*2*10)*2 *(10/freq)); x++ ){
				sinline.AddPoint(new Vector2( (x) *freq ,  Mathf.Sin(x/10f)*AMP  *-1  + 125 ));
			}

			//dots that tell the x point
			for (int dots = 0; dots<length; dots++){
				ColorRect dot = sin.GetNode<ColorRect>("Dot").Duplicate() as ColorRect ;
				xline.AddChild(dot);
				dot.Position = new Vector2( 46 + (dots*linespace) , -3 );
				dot.GetNode<Label>("Number").Text = dots.ToString();
			}


			for (int dots = 0; dots<length/2; dots++){
				ColorRect dot = sin.GetNode<ColorRect>("Dot").Duplicate() as ColorRect ;
				dot.Modulate = dot.Modulate with {R = 0.2f};
				xline.AddChild(dot);
				dot.Position = new Vector2( 46 + (dots*(linespace*6.28f)) , -3 );
				dot.GetNode<Label>("Number").Position = new Vector2(0,-28);
				dot.GetNode<Label>("Number").Text = dots.ToString() + "π" ;
			}
			for (int dots = -1; dots<length/2; dots++){
				ColorRect dot = sin.GetNode<ColorRect>("Dot").Duplicate() as ColorRect ;
				dot.Modulate = dot.Modulate with {R = 0.2f};
				yline.AddChild(dot);
				dot.Position = new Vector2( 0 , (dots*(linespace*6.28f))+125 );
				dot.GetNode<Label>("Number").Position = new Vector2(0,-28);
				dot.GetNode<Label>("Number").Text = dots.ToString() + "π" ;
			}
		}
	}

	public override void _Ready()
	{
		sin = GetNode<ColorRect>("Sin");
		freqslider = GetNode<Slider>("FreqSlider");
		ampslider = GetNode<Slider>("AmpSlider");
		xline = sin.GetNode<ColorRect>("XLine");
		yline = sin.GetNode<ColorRect>("YLine");
		sinline = sin.GetNode<Line2D>("Line");
		fpslabel = GetNode<Label>("FPSCounter");
		sinline.ClearPoints();
		drawtimer = GetNode<Timer>("DrawTimer");

		drawgraph();
		
	}


	private void _on_freq_slider_value_changed(float s){
		drawgraph();
	}

	private void _on_amp_slider_value_changed(float s){
		drawgraph();
	}

	public override void _Process(double delta)
	{

		fpslabel.Text = (Math.Round(1/delta) ).ToString() + " FPS";
	}
}
