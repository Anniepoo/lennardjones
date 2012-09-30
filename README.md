lennardjones
============

Demo of Lennard-Jones attraction

Simple demo of what game ai folks call Lennard-Jones interaction,
which is only vaguely related to what physicists call Lennard-Jones forces.

all particles are attracted 

Directions:

UI - make zero or more particles leaders by right clicking on them. 
Right click leaders to demote.

all particles are attracted by lennard jones.
Leaders also follow the mouse by a Zeno's paradox motion

default is aristotelian motion (v proportional to f)
check newtonian to change to a proportional to f, add friction with
the friction box

A and B depend on:

1) if 'two sides' is selected, whether the particle's index mod 2 is same or different
2) if one or both are leaders

r is euclidean unless the 'manhatten' box is checked.
manhatten dist = abs(x2-x1) + abs(y2 - y1)


