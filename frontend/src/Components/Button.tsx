interface IProps {
  name: string;
  caption: string;
  callFunc?: () => void;
  buttonClass?:string;
}
const Button = (props: IProps) => {
  return (
    <>
      <button
        className={props.buttonClass}
        type="button"
        name={props.name}
        onClick={props.callFunc}
      >
        {" "}
        {props.caption}{" "}
      </button>
    </>
  );
};

export default Button;
