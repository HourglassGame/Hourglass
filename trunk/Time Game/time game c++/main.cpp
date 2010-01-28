#include <allegro.h>

#include <direct.h> // for getcwd
#include <stdio.h>
#include <stdlib.h>// for MAX_PATH
#include <iostream> // for strcat
#include <ctime> // for timing

// timing
double elpased_time;
clock_t start_timer,finish_timer;

BITMAP *xSprite;

int main(){
         
    start_timer = clock();
           
    allegro_init();
    install_keyboard();
    set_color_depth(32);
    set_gfx_mode( GFX_AUTODETECT_WINDOWED, 1024, 768, 0, 0); 
    // GFX_AUTODETECT as first param for fullscreen
    // GFX_AUTODETECT_WINDOWED as first param for windowed
    char CurrentPath[_MAX_PATH];
    getcwd(CurrentPath, _MAX_PATH);
    
    // how to load images
    xSprite = load_bitmap(strcat(CurrentPath,"/resources/images/testlevel.bmp"), NULL);
    
    // check if it's loaded
    if (!xSprite) {
        textout_ex( screen, font, "fail"  , 10, 10, makecol( 255, 255, 255), makecol( 0, 0, 0) );   
    }
    textout_ex( screen, font, "@", 50, 50, makecol( 255, 0, 0), makecol( 0, 0, 0) );

    acquire_screen(); // put in front of draw area
    //textout_ex( screen, font, CurrentPath  , 10, 10, makecol( 255, 255, 255), makecol( 255, 255, 255) );
    
    draw_sprite( screen, xSprite, 0, 0);
   
    finish_timer = clock();    
    elpased_time = (double(finish_timer)-double(start_timer))/CLOCKS_PER_SEC;

    // game loop timing
    char testString[20];
    gcvt(elpased_time, 10, testString); // arg3(string) = arg1(double) to string with arg2(int) figures.

    textout_ex( screen, font, testString, 150, 150, makecol( 255, 0, 0), makecol( 0, 0, 0) );
    release_screen(); // put at the end of drawing

     while( !key[KEY_ESC]){
     
        // game loop
   
    }  

    readkey();
    
    return 0;
    
}   
END_OF_MAIN();
