#include <allegro.h>

#include <direct.h> // for getcwd
#include <stdio.h>
#include <stdlib.h>// for MAX_PATH
#include <iostream> // for strcat
#include <ctime> // for timing

// timing
double elpased_time;
clock_t start_timer,finish_timer;

BITMAP* foreground;
BITMAP* buffer;



void Draw()
{
    acquire_screen();
    draw_sprite( screen, buffer, 0, 0);
    release_screen();
}

int main(){
      
    allegro_init();
    install_keyboard();
    set_color_depth(32);
    set_gfx_mode( GFX_AUTODETECT_WINDOWED, 1024, 768, 0, 0); 
    // GFX_AUTODETECT as first param for fullscreen
    // GFX_AUTODETECT_WINDOWED as first param for windowed
    
    // Get working directory
    char CurrentPath[_MAX_PATH];
    getcwd(CurrentPath, _MAX_PATH);
    
    // how to load images
    foreground = load_bitmap(strcat(CurrentPath,"/resources/images/testlevel.bmp"), NULL);
    buffer = create_bitmap( 1024, 768);
    
    // how to do text textout_ex( screen, font, "@", 50, 50, makecol( 255, 0, 0), makecol( 0, 0, 0) );
    
    double step_interval = 0.029*CLOCKS_PER_SEC;
    draw_sprite( buffer, foreground, 0, 0);

    // game loop timing
    // arg3(string) = arg1(double) to string with arg2(int) figures.
    
    
    
    int count = 0;
    // Game Loop
    start_timer = clock();
    
    while( !key[KEY_ESC]){
        
        finish_timer = clock();
        elpased_time = (double(finish_timer)-double(start_timer));
        
        if (elpased_time >= step_interval) {
            
            start_timer = clock();
            rectfill( buffer, 100, 100, 250, 250, makecol ( 0, 0, 0));
            
            char testString[20];
            gcvt(elpased_time, 10, testString);
            textout_ex( buffer, font, testString, 150, 150, makecol( 255, 0, 0), makecol( 0, 0, 0) );
            
            count ++;
            sprintf(testString,"%d",count);
            textout_ex( buffer, font, testString, 150, 200, makecol( 255, 0, 0), makecol( 0, 0, 0) );
            
            Draw();
            
        }
    }  

    destroy_bitmap( foreground);
    destroy_bitmap( buffer);
    
    //readkey();
    
    return 0;
    
}   
END_OF_MAIN();
