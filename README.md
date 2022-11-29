# Endpoints

## GET /api/categories
Returns all categories
<br/><br/>
Example response body:
```
{
    "statusCode":200,
    "statusDescription":"API call successful",
    "returnedCategories":[
        {
            "categoryId":1,
            "categoryName":"Studying"
        },
        {
            "categoryId":2,
            "categoryName":"Chores"
        },
        {
            "categoryId":3,
            "categoryName":"Assignments"
        }
    ],
    "returnedToDoTasks":null,
    "returnedSteps":null
}
```

## GET /api/tasks
Returns all tasks
<br/><br/>
Example response body:
```
{
    "statusCode":200,
    "statusDescription":"API call successful",
    "returnedCategories":null,
    "returnedToDoTasks":[
        {
            "toDoTaskId":1,
            "toDoTaskText":"Study for final",
            "isComplete":true,
            "categoryId":1
        },
        {
            "toDoTaskId":2,
            "toDoTaskText":"Work on project",
            "isComplete":false,
            "categoryId":1
        },
        {
            "toDoTaskId":3,
            "toDoTaskText":"Complete assignment",
            "isComplete":true,
            "categoryId":2
        },
        {
            "toDoTaskId":4,
            "toDoTaskText":"Fold laundry",
            "isComplete":false,
            "categoryId":3
        }
    ],
    "returnedSteps":null
}
```

## GET /api/tasks/fromCategory/{categoryId}
Returns all tasks belonging to a certain category
<br/><br/>
Example response body:
```
{
    "statusCode":200,
    "statusDescription":"API call successful",
    "returnedCategories":null,
    "returnedToDoTasks":[
        {
            "toDoTaskId":1,
            "toDoTaskText":"Study for final",
            "isComplete":true,
            "categoryId":1
        },
        {
            "toDoTaskId":2,
            "toDoTaskText":"Work on project",
            "isComplete":false,
            "categoryId":1
        }
    ],
    "returnedSteps":null
}
```

## GET /api/tasks/incomplete
Returns all incomplete tasks
<br/><br/>
Example response body:
```
{
    "statusCode":200,
    "statusDescription":"API call successful",
    "returnedCategories":null,
    "returnedToDoTasks":[
        {
            "toDoTaskId":2,
            "toDoTaskText":"Work on project",
            "isComplete":false,
            "categoryId":1
        },
        {
            "toDoTaskId":4,
            "toDoTaskText":"Fold laundry",
            "isComplete":false,
            "categoryId":3
        }
    ],
    "returnedSteps":null
}
```

## GET /api/steps/fromTask/{taskId}
Returns all steps belonging to a certain task
<br/><br/>
Example response body:
```
{
    "statusCode":200,
    "statusDescription":"API call successful",
    "returnedCategories":null,
    "returnedToDoTasks":null,
    "returnedSteps":[
        {
            "stepId": 5,
            "stepText": "Write proposal",
            "isComplete": true,
            "toDoTaskId": 2
        },
        {
            "stepId": 6,
            "stepText": "Make database",
            "isComplete": true,
            "toDoTaskId": 2
        },
        {
            "stepId": 7,
            "stepText": "Plan API endpoints",
            "isComplete": true,
            "toDoTaskId": 2
        },
        {
            "stepId": 7,
            "stepText": "Make models",
            "isComplete": false,
            "toDoTaskId": 2
        },
        {
            "stepId": 7,
            "stepText": "Make controllers",
            "isComplete": false,
            "toDoTaskId": 2
        }
    ]
}
```

## GET /api/steps/fromTask/{taskId}/incomplete
Returns all incomplete steps belonging to a certain task
<br/><br/>
Example response body:
```
{
    "statusCode":200,
    "statusDescription":"API call successful",
    "returnedCategories":null,
    "returnedToDoTasks":null,
    "returnedSteps":[
        {
            "stepId": 7,
            "stepText": "Make models",
            "isComplete": false,
            "toDoTaskId": 2
        },
        {
            "stepId": 7,
            "stepText": "Make controllers",
            "isComplete": false,
            "toDoTaskId": 2
        }
    ]
}
```

## POST /api/categories
Add a new category
<br/><br/>
Example request body:
```
{
    "categoryname":"Interview prep"
}
```

## POST /api/tasks
Add a new task
<br/><br/>
Example request body:
```
{
    "todotasktext":"Do 10 practice problems",
    "iscomplete":false,
    "categoryid":4
}
```

## POST /api/steps
Add a new step
<br/><br/>
Example request body:
```
{
    "steptext":"Review binary trees",
    "iscomplete":false,
    "todotaskid":5
}
```

## PUT /api/tasks/{id}
Update a task
<br/><br/>
Example request body:
```
{
    "todotasktext":"Do 10 practice problems",
    "iscomplete":true,
    "categoryid":4
}
```

## PUT /api/steps/{id}
Update a step
<br/><br/>
Example request body:
```
{
    "steptext":"Review binary trees",
    "iscomplete":true,
    "todotaskid":5
}
```