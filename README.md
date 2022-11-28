# Endpoints

## GET /api/categories
Returns all categories
<br/>
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

## GET /api/tasks/fromCategory/{categoryId}
Returns all tasks belonging to a certain category

## GET /api/tasks/incomplete
Returns all incomplete tasks

## GET /api/steps/fromTask/{taskId}
Returns all steps belonging to a certain task

## GET /api/steps/ fromTask/{taskId}/incomplete
Returns all incomplete steps belonging to a certain task

## POST /api/categories
Add a new category

## POST /api/tasks
Add a new task

## POST /api/steps
Add a new step

## PUT /api/tasks/{id}
Update a task

## PUT /api/steps/{id}
Update a step