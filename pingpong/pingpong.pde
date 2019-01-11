void setup() {
  size(600, 400); // adjustable size
  background(0);
  rectMode(CENTER);
  fill(100, 100, 100);
  rect(width/2, height/2, width-100, height-100);
  fill(0);
  textSize(width/40);
  textAlign(CENTER, CENTER);
  text("To control the left paddle use A and Z to go up and down", width/2, height/4); // starting instructions
  text("To control the right paddle hold the left mouse button" , width/2, 6*height/16);
  text("and move it in the desired direction", width/2, 7*height/16, 6*height/16);
  textSize(width/36);
  text("Please press enter followed by space to start", width/2, 3*height/4);
}
float rectX1;
float rectX2;
float rectW;
float pHeight;
float qHeight;
float ballSpeed;
float ballSpeedX;
float ballSpeedY = 0;
float speed;
boolean start=false;
float ballX;
float ballY;
int scoreX1=0;
int scoreX2=0;
float flip=1;
void draw() {

  if (key==ENTER) {
    start=true;
    rectX1=width-width/20; // right paddle x coord
    rectX2=width/20; // left paddle x coord
    rectW=width/30; // width of the paddle
    pHeight = height/2; // saved height of right paddle
    qHeight = height/2; // "             " left paddle
    ballX=width/2; // saved x coord
    ballY=height/2; // saved y coord
    speed=height/60;
    ballSpeed=width/100; // the saved speed of the ball
    ballSpeedY=0; // the saved speed of the ball with regards to the y axis
    ballSpeedX=ballSpeed; // the saved speed of the ball with regards to the x axis
    scoreX1=0; // the saved scores of the two players
    scoreX2=0;
  }
  if (start == true) { // run if the player presses enter 
    fill(0);
    rectMode(CORNERS);
    rect(0, 0, width, height);
    rectMode(CENTER);
    fill(255);
    text("Press enter to reset and space to start", width/2, height/20);
    pHeight=constrain(pHeight, height/12,height*11/12); // constraining the paddles to within the window
    qHeight=constrain(qHeight,height/12,height*11/12);
    ballSpeed=constrain(ballSpeed,width/99,width/100*1.70); // constrain ball speed to avoid an error when increasing it phases through paddles

    if (mousePressed==true) { // if the mouse is above or below the paddle and is being pressed,
      if (mouseY<pHeight) {  // then the paddle will move in the direction of the mouse at a specified speed
        pHeight=pHeight-speed; // mouse above
        rect(rectX1, pHeight+speed, rectW, height/6);
      }
      if (mouseY>pHeight) {
        pHeight+=speed; // mouse below
        rect(rectX1, pHeight, rectW, height/6);
      } else if (mouseY==pHeight) {
       rect(rectX1, pHeight, rectW, height/6);
    } 
    } else { // if even with the mouse draw a rectangle at the location of the mouse
      rect(rectX1, pHeight, rectW, height/6);
    }

    if (keyPressed==true) { 
      if (key == 'a' || key == 'A') { // if the 'a' or 'A' key is pressed the left paddle will move up at the specified speed
        qHeight=qHeight-speed;
        rect(rectX2, qHeight, rectW, height/6);
      } else if (key == 'z' || key == 'Z') { // if the 'z' or 'Z' "                               " down at the specified speed
        qHeight+=speed;
        rect(rectX2, qHeight, rectW, height/6);
      } else {
        rect(rectX2, qHeight, rectW, height/6); // if a key is pressed but isn't either paddle at previously recorded height
      }
    } else {
      rect(rectX2, qHeight, rectW, height/6); // if no key is pressed paddle is at previously recorded qHeight
    }
    
    if (ballY>height || ballY<0) { // if the ball hits the top or bottom, xSpeed is unaffected but ySpeed makes it "rebound"
      ballSpeedY= ballSpeedY*-1;
    }
    if (ballX<0-height/60) { // if goes off the left side, right side gets a point and it resets with random angle
      scoreX1++;            // and orignal speed.
      ballX=width/2;
      ballY=height/2;
      ballSpeed=width/100;
      ballSpeedX=random(0,ballSpeed/2);
      flip=random(0,2);
      if (flip>=1) {
      ballSpeedY=sqrt(pow(ballSpeed,2)-pow(ballSpeedX,2));
      }
      if (flip<1) {
        ballSpeedY=-1*sqrt(pow(ballSpeed,2)-pow(ballSpeedX,2));
      }
    }
    if (ballX>width+height/60) { // if goes of the right side, left side ...
      ballX=width/2;
      ballY=height/2;
      scoreX2++;
      ballSpeed=width/100;
      ballSpeedX=random(-ballSpeed/2,0);
      flip=random(0,2);
      if (flip>=1) {
      ballSpeedY=sqrt(pow(ballSpeed,2)-pow(ballSpeedX,2));
      }
      if (flip<1) {
        ballSpeedY=-1*sqrt(pow(ballSpeed,2)-pow(ballSpeedX,2));
      }
    }
    
    // each paddle is divided into 9 pieces of height== window height/54, the edge pieces have height 3 window height/108 to
    // ensure it appear the ball still hits when only a small portion of the ball overlaps. 
      
      if (ballX>=rectX1-rectW/2-height/60 && ballX<rectX1-rectW/2 && ballY>pHeight-height/108 && ballY<pHeight+height/108) {
        ballSpeedX=ballSpeedX*-1; // the following are all in the same for of: if hits paddle, inverses x speed. "rebounds"
        ballSpeedX=ballSpeed*cos(PI); // ball speed * cos of desired angle gives the x trajectory of ball in a single increment
        ballSpeedY=ballSpeed*sin(PI); // ball speed * sin of desired angle gives the y "                                       "
        ballSpeed=ballSpeed*1.05; // increase the ball speed every time the ball hits a paddle. 
      }
      if (ballX>=rectX1-rectW/2-height/60 && ballX<rectX1-rectW/2 && ballY<=pHeight-height/108 && ballY>pHeight-3*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(-9*PI/10);
        ballSpeedY=ballSpeed*sin(-9*PI/10);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX>=rectX1-rectW/2-height/60 && ballX<rectX1-rectW/2 && ballY<=pHeight-3*height/108 && ballY>pHeight-5*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(-4*PI/5);
        ballSpeedY=ballSpeed*sin(-4*PI/5);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX>=rectX1-rectW/2-height/60 && ballX<rectX1-rectW/2 && ballY<=pHeight-5*height/108 && ballY>pHeight-7*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(-7*PI/10);
        ballSpeedY=ballSpeed*sin(-7*PI/10);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX>=rectX1-rectW/2-height/60 && ballX<rectX1+rectW/2+height/60 && ballY<=pHeight-7*height/108 && ballY>pHeight-10*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(-3*PI/5);
        ballSpeedY=ballSpeed*sin(-3*PI/5);
        ballSpeed=ballSpeed*1.05;
      }

      if (ballX>=rectX1-rectW/2-height/60 && ballX<rectX1-rectW/2 && ballY>=pHeight+height/108 && ballY<pHeight+3*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(9*PI/10);
        ballSpeedY=ballSpeed*sin(9*PI/10);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX>=rectX1-rectW/2-height/60 && ballX<rectX1-rectW/2 && ballY>=pHeight+3*height/108 && ballY<pHeight+5*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(4*PI/5);
        ballSpeedY=ballSpeed*sin(4*PI/5);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX>=rectX1-rectW/2-height/60 && ballX<rectX1-rectW/2 && ballY>=pHeight+5*height/108 && ballY<pHeight+7*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(7*PI/10);
        ballSpeedY=ballSpeed*sin(7*PI/10);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX>=rectX1-rectW/2-height/60 && ballX<rectX1+rectW/2+height/60 && ballY>=pHeight+7*height/108 && ballY<pHeight+10*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(3*PI/5);
        ballSpeedY=ballSpeed*sin(3*PI/5);
        ballSpeed=ballSpeed*1.05;
      }
      
      
      if (ballX<rectX2+rectW/2+height/60 && ballX>=rectX2-rectW/2 && ballY>qHeight-height/108 && ballY<qHeight+height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(0);
        ballSpeedY=ballSpeed*sin(0);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX<rectX2+rectW/2+height/60 && ballX>=rectX2-rectW/2 && ballY<=qHeight-height/108 && ballY>qHeight-3*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(-PI/10);
        ballSpeedY=ballSpeed*sin(-PI/10);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX<rectX2+rectW/2+height/60 && ballX>=rectX2-rectW/2 && ballY<=qHeight-3*height/108 && ballY>qHeight-5*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(-PI/5);
        ballSpeedY=ballSpeed*sin(-PI/5);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX<rectX2+rectW/2+height/60 && ballX>=rectX2-rectW/2 && ballY<=qHeight-5*height/108 && ballY>qHeight-7*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(-3*PI/10);
        ballSpeedY=ballSpeed*sin(-3*PI/10);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX<rectX2+rectW/2+height/60 && ballX>=rectX2-rectW/2-height/60 && ballY<=qHeight-7*height/108 && ballY>qHeight-10*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(-2*PI/5);
        ballSpeedY=ballSpeed*sin(-2*PI/5);
        ballSpeed=ballSpeed*1.05;
      }

      if (ballX<rectX2+rectW/2+height/60 && ballX>=rectX2-rectW/2 && ballY>=qHeight+height/108 && ballY<qHeight+3*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(PI/10);
        ballSpeedY=ballSpeed*sin(PI/10);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX<rectX2+rectW/2+height/60 && ballX>=rectX2-rectW/2 && ballY>=qHeight+3*height/108 && ballY<qHeight+5*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(PI/5);
        ballSpeedY=ballSpeed*sin(PI/5);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX<rectX2+rectW/2+height/60 && ballX>=rectX2-rectW/2 && ballY>=qHeight+5*height/108 && ballY<qHeight+7*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(3*PI/10);
        ballSpeedY=ballSpeed*sin(3*PI/10);
        ballSpeed=ballSpeed*1.05;
      }
      if (ballX<rectX2+rectW/2+height/60 && ballX>=rectX2-rectW/2-height/60 && ballY>=qHeight+9*height/108 && ballY<qHeight+10*height/108) {
        ballSpeedX=ballSpeedX*-1;
        ballSpeedX=ballSpeed*cos(2*PI/5);
        ballSpeedY=ballSpeed*sin(2*PI/5);
        ballSpeed=ballSpeed*1.05;
      }
      

    ballY+=ballSpeedY; // the prev y coord + the y increment determined by ballspeed * sin...
    ballX+=ballSpeedX; // the prev x coord + the x increment "                      "* cos...
    
    text(scoreX1,width-width/10,height-height/20); // displays the scores. 
    text(scoreX2,width/10,height-height/20);
     
    ellipse(ballX, ballY, height/30, height/30);  // the ball
  }
}