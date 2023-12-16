import { authService } from "./APIs/Services/AuthService";
import { examService } from "./APIs/Services/ExamService";
import examTypeService from "./APIs/Services/ExamTypeService";
import facultyService from "./APIs/Services/FacultyService";
import groupService from "./APIs/Services/GroupService";
import { groupSubjectService } from "./APIs/Services/GroupSubjectService";
import { roleService } from "./APIs/Services/RoleService";
import { studentService } from "./APIs/Services/StudentService";
import { SubjectService } from "./APIs/Services/SubjectService";
import { TeacherRoleService } from "./APIs/Services/TeacherRoleSerivce";
import { LessonTypeService } from "./APIs/Services/LessonTypeService";
import teacherService from "./APIs/Services/TeacherService";
import { userService } from "./APIs/Services/UserService";
const useService = () => {
  const studentServices = new studentService();
  const groupServices = new groupService();
  const teacherServices = new teacherService();
  const facultyServices = new facultyService();
  const examTypeServices = new examTypeService();
  const subjectServices = new SubjectService();
  const teacherRoleServices = new TeacherRoleService();
  const lessonTypeServices = new LessonTypeService();
  const examServices = new examService();
  const groupSubjectServices = new groupSubjectService();
  const userServices = new userService();
  const authServices = new authService();
  const roleServices = new roleService();
  return {
    studentServices,
    groupServices,
    teacherServices,
    facultyServices,
    examTypeServices,
    subjectServices,
    teacherRoleServices,
    lessonTypeServices,
    examServices,
    groupSubjectServices,
    userServices,
    authServices,
    roleServices,
  };
};
export default useService;