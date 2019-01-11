void setup() {
  size(300, 300);
  background(0);

  fill(255); // setting up the white box, seconds and numbers
  rectMode(CORNERS);
  rect(100, 50, width-50, height-50);
  fill(255);
  textAlign(CENTER);
  text("Hours", 50, 50+(height-100)/12*2); // the height of the white box is divided into 12 parts to
  text("Minutes", 50, 50+(height-100)/12*6); // space out bars and words vertically
  text("Seconds", 50, 50+(height-100)/12*10);
  for (int i=0; i<13; i=i+1) { // label the increments on the top and bottom
    String s= Integer.toString(i);
    text(s, 100+i*((width-150)/12), 25); // the width of the white box divided by hours
    int z= i*5;
    if (i%2==0) {
      String t= Integer.toString(z);
      text(t, 100+z*(width-150)/60, height-25); // the width of the white box divide by minutes/seconds
    }
  }
}

int fill=60; // defining variable that are used in the draw function
int filla=220;
int fillb=60;
int fillc=220;
int ps=100;
int ph=100;
int pm=100;
int j=0;
int k=0;

void draw() {
  noStroke();
  int hours= hour()%12;   // defining variables that need to be updated as the time changes
  int minutes= minute();
  int seconds= second();
  String h = Integer.toString(hours);
  String m = Integer.toString(minutes);
  String sec = Integer.toString(seconds);
  int nh = 100+(hours)*(width-150)/12; // the current time * the width of the white box
  int nm = 100+(minutes)*(width-150)/60; // divided by the number of time increments
  int ns = 100+(seconds)*(width-150)/60;

  if (ps!=ns) { // making colors update every second but not in between seconds 
    if (fill<=225) { // also making alternating color gradients
      fill+=4;
      fillb=fill;
    } else if (fill>225 && filla>45) {
      filla=filla-4;
      fillc=filla;
    } 
    if (filla<=45 && fillb>45) {
      fillb=fillb-4;
    } else if (fillb<=45) {
      fillc=fillc+4;
    } 
    if (fillc>225) {
      fill=60;
      fillb=60;
      filla=220;
      fillc=220;
    }
  }

  if (seconds==0) {  // reseting the time bar once it reaches a new hour, minute or second. 
    fill(255);
    rect(101, 50+(height-100)/12*9, width-50, 50+(height-100)/12*11);
  }
  if (minutes==0) {
    fill(255);
    rect(101, 50+(height-100)/12*5, width-50, 50+(height-100)/12*7);
  }
  if (hours==0) {
    fill(255);
    rect(101, 50+(height-100)/12*1, width-50, 50+(height-100)/12*3);
  }

  fill(255); // adding the initial bar for hours
  rect(ph+12, 50+(height-100)/12*1, ph+38, 50+(height-100)/12*3);
  fill(180, fillb, fillc);
  text(h, 25+nh, 50+(height-100)/12*2);
  rect(ph, 50+(height-100)/12*1, nh, 50+(height-100)/12*3);

  fill(255); //covering up the previous time stamp
  rect(pm+12, 50+(height-100)/12*5, pm+38, 50+(height-100)/12*7);
  fill(0); //covering up the previous time stamp if it goes past the edge of the white box
  rect(width, height-50, width-50, 50);
  fill(40, fillb, fillc);// adding a new bar in a different color every time the minute changes
  text(m, 25+nm, 50+(height-100)/12*6);
  rect(pm, 50+(height-100)/12*5, nm, 50+(height-100)/12*7);

  fill(255); //covering up the previous time stamp
  rect(ps+12, 50+(height-100)/12*9, ps+38, 50+(height-100)/12*11);
  fill(0); //covering up the previous time stamp if it goes past the edge of the white box
  rect(width, height-50, width-50, 50);
  fill(40, fillc, fillb); // adding a new bar in a different color every time the minute changes
  text(sec, 25+ns, 50+(height-100)/12*10);
  rect(ps, 50+(height-100)/12*9, ns, 50+(height-100)/12*11);

  if (minutes==0) { // displaying a moving message when it becomes a new hour. 
    frameRate(1);
    if (hours%12==0) {
      h="12";
    }
    fill(0);
    rect(0, 0, width, height);
    fill(255);
    text("It is now "+h+"!", random(50, width-50), random(50, height-50));
  }
  if (minutes==1 && seconds==0) { // at a minute past the hour, reseting the setup
    frameRate(30);
    fill(255);
    rect(100, 50, width-50, height-50);
    text("Hours", 50, 50+(height-100)/12*2);
    text("Minutes", 50, 50+(height-100)/12*6);
    text("Seconds", 50, 50+(height-100)/12*10);
    for (int i=0; i<13; i=i+1) {
      String s= Integer.toString(i);
      text(s, 100+i*((width-150)/12), 25);
      int z= i*5;
      if (i%2==0) {
        String t= Integer.toString(z);
        text(t, 100+z*(width-150)/60, 425);
      }
    }
    for (int i=0; i<=hours; i+=1) { // updating the hours bar after the message is displayed
      j=nh;
      k=nh-(hours-i)*(width-150)/12;
      fill(225-16*i, 90, 55+16*i);
      rect(j, 50+(height-100)/12*1, k, 50+(height-100)/12*3);
    }
    text(h, 25+nh, 50+(height-100)/12*2);
    fill(40, fillb, fillc); // adding in the first minute of the hour. 
    rect(100, 50+(height-100)/12*5, nm, 50+(height-100)/12*7);
    text(m, 25+nm, 50+(height-100)/12*6);
  }

  ps=ns; // making the previous time stamp equal the new time stamp for
  ph=nh; // creating a new bar every time update. 
  pm=nm;
}