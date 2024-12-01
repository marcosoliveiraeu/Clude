import React from "react";
import "./css/ModalCustom.css";

const ExcluirProfissionalModal = ({ show, onClose, onConfirm, profissional }) => {
  if (!profissional) return null;

  return (
    <div className="modal-overlay-custom" tabIndex="-1">
        <div className="modal-dialog-custom">           

            <div className="modal-content-custom">
                <header className="modal-header-custom">
                    <h5>Excluir profissional</h5>
                    <button type="button" className="btn-close"  onClick={onClose}></button> 
                </header>

                <hr></hr>

                <div className="modal-body-custom">
                    Tem certeza que deseja excluir o profissional {profissional.nome} - 
                    ({profissional.tipoDocumento} {profissional.numeroDocumento})? 
                    Essa ação não poderá ser desfeita!
                </div>
               
               <hr></hr>

                <div className="modal-footer-custom">
                    <button type="button" className="btn btn-secondary" onClick={onClose}>Cancelar</button>

                    <button className="btn btn-danger" onClick={onConfirm}>
                        Sim, excluir
                    </button>
                </div>
            </div>

        </div>        
    </div>
  );
};

export default ExcluirProfissionalModal;
