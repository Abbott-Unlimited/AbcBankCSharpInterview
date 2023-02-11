<!-- 

  1 space -- &nbsp;
  2 spaces -- &ensp;
  4 spaces -- &emsp;
-->


### Icon Meanings (Legend)


|                   |   <sup>shortrcode[^1]</sup>                           |   <sup>scope</sup>                                    |   <sup>description</sup>                                                      | 
| ----------------- | ----------------------------------------------------- | ----------------------------------------------------- | ----------------------------------------------------------------------------- | 
|   <sup>⭕</sup>   |   <sup><sub>`:o:`</sub></sup>                         |   <sup><sub>Task Group</sup></sub>                    |   <sup><sub>one or more failed child tasks</sub></sup>                        |
|   <sup>🔻</sup>   |   <sup><sub>`:small_red_triangle_down:`</sub></sup>   |   <sup><sub>Task or Feature + children</sub></sup>    |   <sup><sub>de-prioritized item.  Will not be worked.</sub></sup>             |
|   <sup>⚠️</sup>   |   <sup><sub>`:warning:`</sub></sup>                   |   <sup><sub>Any</sub></sup>                           |   <sup><sub>attention - additional information within.</sub></sup>            |
|   <sup>❌</sup>   |   <sup><sub>`:x:`</sub></sup>                         |   <sup><sub>tasks, subtasks</sub></sup>               |   <sup><sub>failed or missing task/subtask</sub></sup>                        |
|   <sup>⚫</sup>   |   <sup><sub>`:black_circle:`</sub></sup>              |   <sup><sub>feature, task, subtask</sub></sup>        |   <sup><sub>Planned Not Started</sub></sup>                                   |
|   <sup>🟤</sup>   |   <sup><sub>`:brown_circle:`</sub></sup>              |   <sup><sub>feature, task, subtask</sub></sup>        |   <sup><sub>Blocked</sub></sup>                                               |
|   <sup>🟠</sup>   |   <sup><sub>`:orange_circle:`</sub></sup>             |   <sup><sub>feature, task, subtask</sub></sup>        |   <sup><sub>In Progress - started, dependencies not completed.</sub></sup>    |
|   <sup>🟡</sup>   |   <sup><sub>`:yellow_circle:`</sub></sup>             |   <sup><sub>feature, task, subtask</sub></sup>        |   <sup><sub>In Progress - partial implementation</sub></sup>                  |
|   <sup>🟢</sup>   |   <sup><sub>`:green_circle:`</sub></sup>              |   <sup><sub>feature, task, subtask</sub></sup>        |   <sup><sub>In Progress - Subtask or feature dependency complete</sub></sup>  |
|   <sup>✔️</sup>   |   <sup><sub>`:heavy_check_mark:`</sub></sup>          |   <sup><sub>task, subtask</sub></sup>                 |   <sup><sub>Complete - Work item, Task or Sub Task complete.</sub></sup>      |
|   <sup>✅</sup>   |   <sup><sub>`:white_check_mark:`</sub></sup>          |   <sup><sub>Epic, Feature or Task Group</sub></sup>   |   <sup><sub>Feature or Task Group complete</sub></sup>                        |


[github flavored markdown supported emojis](https://github.com/ikatyang/emoji-cheat-sheet/blob/master/README.md)

[^1]: **Emoji shortcodes do not work in some situations** 
  eg:
    ```markdown
    <detail>
      <summary>:white_check_mark: My Section</summary> 
      ...
    </detail>
    ``` 
    will display
    &emsp;&emsp;`:white_check_mark:` My Completed Feature, 
    and not
    &emsp;&emsp;:white_check_mark: My Completed Feature

      