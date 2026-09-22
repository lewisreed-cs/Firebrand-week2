# API Test Results

| Endpoint | Scenario | Request | Expected Status | Actual Status | Pass/Fail |
|-----------|-----------|-----------|-----------|-----------|-----------|
| GET /api/students | Get all students | None | 200 OK | 200 OK | Pass |
| GET /api/students/{id} | Existing ID | 1 | 200 OK | 200 OK | Pass |
| GET /api/students/{id} | Non-existent ID | 999 | 404 Not Found | 404 Not Found | Pass |
| POST /api/students | Valid student | Name="Lewis", Score=85 | 201 Created | 201 Created | Pass |
| POST /api/students | Empty name | Name="", Score=85 | 400 Bad Request | 400 Bad Request | Pass |
| PUT /api/students/{id} | Valid update | Existing ID, valid data | 200 OK | 200 OK | Pass |
| PUT /api/students/{id} | Non-existent ID | 999 | 404 Not Found | 404 Not Found | Pass |
| DELETE /api/students/{id} | Existing ID | 1 | 204 No Content | 204 No Content | Pass |
| DELETE /api/students/{id} | Same ID again | 1 | 404 Not Found | 404 Not Found | Pass |

## Summary

All endpoints were tested through Swagger UI.

- GET endpoints correctly returned 200 for valid requests and 404 for missing resources.
- POST correctly returned 201 for valid data and 400 for invalid data.
- PUT correctly returned 200 for valid updates and 404 for non-existent resources.
- DELETE correctly returned 204 when deleting an existing resource and 404 when attempting to delete the same resource again.

### Issues Found

None. All endpoints behaved as expected.