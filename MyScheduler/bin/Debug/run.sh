#params:  startTime  endTime  program  parameters
#ie  MyScheduler.exe 21:00 22:00 program.exe hoi

mono ./MyScheduler.exe 9:00 9:01  ffmpeg -f pulse -ac 2 -i default -f v4l2 -i /dev/video0 -vcodec libx264 "out/out.avi"

