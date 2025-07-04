﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using WebAPI.Data;
using WebAPI.Entities.DTOs;
using WebAPI.Entities.Models;

namespace WebAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMongoCollection<Student> _students;
        private readonly IMongoCollection<RegistrationCounter> _counters;
        private readonly DatabaseContext _context;

        public StudentService(IMongoDatabase database, DatabaseContext context)
        {
            _students = database.GetCollection<Student>("students");
            _counters = database.GetCollection<RegistrationCounter>("registration_counters");
            _context = context;
        }

        public async Task<StudentResponseDto> CreateStudentAsync(CreateStudentDto createStudentDto)
        {
            var registrationNumber = await GenerateRegistrationNumberAsync(createStudentDto.Course);
            var currentYear = DateTime.UtcNow.Year;

            var student = new Student
            {
                FirstName = createStudentDto.FirstName,
                LastName = createStudentDto.LastName,
                Email = createStudentDto.Email,
                Phone = createStudentDto.Phone,
                CurrentRegistrationNumber = registrationNumber,
                CurrentCourse = createStudentDto.Course,
                RegistrationHistory = new List<RegistrationHistory>
            {
                new RegistrationHistory
                {
                    RegistrationNumber = registrationNumber,
                    Course = createStudentDto.Course,
                    Year = currentYear,
                    RegisteredAt = DateTime.UtcNow,
                    IsActive = true
                }
            }
            };

            await _students.InsertOneAsync(student);
            return MapToStudentResponseDto(student);
        }

        public async Task<List<StudentResponseDto>> GetAllStudentsAsync()
        {
            var students = await _students.Find(_ => true).ToListAsync();
            return students.Select(MapToStudentResponseDto).ToList();
        }

        public async Task<StudentResponseDto> UpdateStudentCourseAsync(UpdateStudentDto updateDto)
        {
            string newRegistrationNumber = "";
            var student = await _students.Find(s => s.Id == updateDto.StudentId).FirstOrDefaultAsync();
            if (student == null)
                throw new ArgumentException("Student not found");
            if(student.CurrentCourse != updateDto.Course)
            {
                newRegistrationNumber = await GenerateRegistrationNumberAsync(updateDto.Course);
            }

            var currentYear = DateTime.UtcNow.Year;

            foreach (var history in student.RegistrationHistory)
            {
                history.IsActive = false;
            }

            student = new Student
            {
                Id = updateDto.StudentId,
                FirstName = updateDto.FirstName,
                LastName = updateDto.LastName,
                Email = updateDto.Email,
                Phone = updateDto.Phone,
                CurrentRegistrationNumber = student.CurrentCourse != updateDto.Course ? newRegistrationNumber : student.CurrentRegistrationNumber,
                CurrentCourse = updateDto.Course,
                UpdatedAt = DateTime.UtcNow,
                RegistrationHistory = new List<RegistrationHistory>
            {
                new RegistrationHistory
                {
                    RegistrationNumber = newRegistrationNumber,
                    Course = updateDto.Course,
                    Year = currentYear,
                    RegisteredAt = DateTime.UtcNow,
                    IsActive = true
                }
            }
            };

            await _students.ReplaceOneAsync(s => s.Id == updateDto.StudentId, student);
            return MapToStudentResponseDto(student);
        }

        //private async Task<string> GenerateRegistrationNumberAsync(string courseCode)
        //{
        //    var currentYear = DateTime.UtcNow.Year;

        //    var filter = Builders<RegistrationCounter>.Filter.And(
        //        Builders<RegistrationCounter>.Filter.Eq(c => c.Course, courseCode),
        //        Builders<RegistrationCounter>.Filter.Eq(c => c.Year, currentYear)
        //    );

        //    var update = Builders<RegistrationCounter>.Update
        //        .Inc(c => c.Counter, 1)
        //        .Set(c => c.LastUpdated, DateTime.UtcNow)
        //        .SetOnInsert(c => c.Course, courseCode)
        //        .SetOnInsert(c => c.Year, currentYear);

        //    var options = new FindOneAndUpdateOptions<RegistrationCounter>
        //    {
        //        IsUpsert = true,
        //        ReturnDocument = ReturnDocument.After
        //    };

        //    var counter = await _counters.FindOneAndUpdateAsync(filter, update, options);
        //    return $"{courseCode}{currentYear}{counter.Counter:D4}";
        //}

        private async Task<string> GenerateRegistrationNumberAsync(string courseCode)
        {
            var currentYear = DateTime.UtcNow.Year;
            var existingCounter = await _context.Courses.FirstOrDefaultAsync(rc => rc.CourseName == courseCode && rc.Year == currentYear);

            if(existingCounter == null)
            {
                existingCounter = new Course
                {
                    CourseName = courseCode,
                    Year = currentYear,
                    Counter = 1,
                    LastUpdated = DateTime.UtcNow,
                };
                _context.Courses.Add(existingCounter);
            }
            else
            {
                existingCounter.Counter += 1;
                existingCounter.LastUpdated = DateTime.UtcNow;
                _context.Courses.Update(existingCounter);
            }
            await _context.SaveChangesAsync();

            return $"{courseCode}{currentYear}{existingCounter.Counter:D4}";
        }

        private StudentResponseDto MapToStudentResponseDto(Student student)
        {
            return new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Phone = student.Phone,
                CurrentCourse = student.CurrentCourse,
                CurrentRegistrationNumber = student.CurrentRegistrationNumber,
                RegistrationHistory = student.RegistrationHistory.Select(r => new RegistrationHistoryDto
                {
                    RegistrationNumber = r.RegistrationNumber,
                    Course = r.Course,
                    Year = r.Year,
                    RegisteredAt = r.RegisteredAt,
                    IsActive = r.IsActive
                }).ToList(),
                CreatedAt = student.CreatedAt
            };
        }


    }
}
