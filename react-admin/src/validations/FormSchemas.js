import * as yup from "yup";

// Account Schemas
export const SignUpSchema = yup.object().shape({
  
    userName: yup
      .string()
      .required("Username is required")
      .max(100, "Username cannot be more than 100 characters"),
  
    email: yup
      .string()
      .required("Email is required")
      .email("Invalid Email Address"),
  
    password: yup
      .string()
      .required("Password is required")
      .min(8, "Password must be at least 8 characters")
      .max(20, "Password cannot exceed 20 characters"),
  
    confirmPassword: yup.string().required("Confirm Password is required"),
    // roleIds: yup
    //   .array()
    //   .required("At least one user role most me provided"),
    // roleIds: yup.array().min(1, "At least one user role most me provided"),

  });

  export const UpdateUserSchema = yup.object().shape({
  
    userName: yup
      .string()
      .required("Username is required")
      .max(100, "Username cannot be more than 100 characters"),
  
    // email: yup
    //   .string()
    //   .required("Email is required")
    //   .email("Invalid Email Address"),
  
    // password: yup
    //   .string()
    //   .required("Password is required")
    //   .min(8, "Password must be at least 8 characters")
    //   .max(20, "Password cannot exceed 20 characters"),
  
    // confirmPassword: yup.string().required("Confirm Password is required"),
    // roleIds: yup
    //   .array()
    //   .required("At least one user role most me provided"),
    // roleIds: yup.array().min(1, "At least one user role most me provided"),

  });
  
  export const SignInSchema = yup.object().shape({
    usernameOrEmail: yup
      .string()
      .required("Username or Email is required")
      .max(100, "Username or Email cannot be more than 100 characters"),
  
    password: yup
      .string()
      .required("Password is required")
      .min(8, "Password must be at least 8 characters long")
      .max(20, "Password cannot be more than 20 characters long"),
  
    rememberMe: yup.boolean(),
  });
  
  export const VerifyAccountSchema = yup.object().shape({
    otp: yup
      .string()
      .required("Otp is required"),
  });
  
  export const ForgotPasswordSchema = yup.object().shape({
    email: yup
      .string()
      .required("Email is required")
      .email("Invalid Email Address"),
  });
  
  export const ResetPasswordSchema = yup.object().shape({
    newPassword: yup
      .string()
      .required("New password is required")
      .min(8, "Password must be at least 8 characters long")
      .max(20, "Password cannot exceed 20 characters"),
    
    confirmPassword: yup
      .string()
      .required("Confirm Password is required")
      .oneOf([yup.ref('newPassword'), null], 'Passwords must match'),
  });

  // Artists Schemas
export const FacultyCreateSchema = yup.object().shape({
  name: yup
    .string()
    .required("Faculty name is required")
    .max(256, "Faculty name cannot exceed 256 characters"),
  // projectIds: yup.array().min(1, "At least one project is required"),
  facultyCode: yup
  .string()
  .required("Faculty name is required")
  .max(256, "Faculty name cannot exceed 256 characters"),
  studySectorName: yup
  .string()
  .required("Faculty name is required")
  .max(256, "Faculty name cannot exceed 256 characters"),
  studySectorCode: yup
  .string()
  .required("Faculty name is required")
  .max(256, "Faculty name cannot exceed 256 characters"),
});

export const FacultyUpdateSchema = yup.object().shape({
  name: yup
    .string()
    .required("Faculty name is required")
    .max(256, "Faculty name cannot exceed 256 characters"),
  // projectIds: yup.array().min(1, "At least one project is required"),
  facultyCode: yup
  .string()
  .required("Faculty name is required")
  .max(256, "Faculty name cannot exceed 256 characters"),
  studySectorName: yup
  .string()
  .required("Faculty name is required")
  .max(256, "Faculty name cannot exceed 256 characters"),
  studySectorCode: yup
  .string()
  .required("Faculty name is required")
  .max(256, "Faculty name cannot exceed 256 characters"),
});

// Artists Schemas
export const ArtistCreateSchema = yup.object().shape({
  artistName: yup
    .string()
    .required("Artist name is required")
    .max(256, "Artist name cannot exceed 256 characters"),
  projectIds: yup.array().min(1, "At least one project is required"),
});