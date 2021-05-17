# Vue  

1. While run vue in chorme, the view will always show a blank spoce at the top of view.

This is caused by default app style, so we just need to delete the style in App.vue;

``` bash

    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px; -- delete it to solve this problem
    }
```
