import React from "react";
import icon3dots from "../icons/3dots.png";
import './css/AcoesButton.css';

const AcoesButton = ({ onEdit, onDelete }) => {
  return (
    <div className="dropdown custom-img">
      <button
        className="btn btn-link p-0"
        type="button"
        data-bs-toggle="dropdown"
        aria-expanded="false"       
      >
        <img
          src={icon3dots}
          alt="Ações"
          style={{ width: "24px", height: "24px" }}
        />
      </button>
      <ul className="dropdown-menu">
        <li>
          <button className="dropdown-item" onClick={onEdit}>
            Editar
          </button>
        </li>
        <li>
          <button className="dropdown-item" onClick={onDelete}>
            Excluir
          </button>         
        </li>
      </ul>
    </div>
  );
};

export default AcoesButton;
